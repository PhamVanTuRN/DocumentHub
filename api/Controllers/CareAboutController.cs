using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/careabout")]
    [ApiController]
    public class CareAboutController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDocumentRepository _documentRepo;
        private readonly ICareAboutRepository _careAboutRepo;

        public CareAboutController(UserManager<AppUser> userManager,
        IDocumentRepository documentRepo, ICareAboutRepository careAboutRepo)
        {
            _userManager = userManager;
            _documentRepo = documentRepo;
            _careAboutRepo = careAboutRepo;

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserCareAbout()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userCareAbout = await _careAboutRepo.GetUserCareAbout(appUser);
            return Ok(userCareAbout);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCareAbout(string documentName)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var document = await _documentRepo.GetByDocumentNameAsync(documentName);

            if (document == null) return BadRequest("Document not found");

            var userCareAbout = await _careAboutRepo.GetUserCareAbout(appUser);

            if (userCareAbout.Any(e => e.DocummentName.ToLower() == documentName.ToLower())) return BadRequest("Cannot add same document to care about");

            var careAboutModel = new CareAbout
            {
                DocumentId = document.Id,
                AppUserId = appUser.Id
            };

            await _careAboutRepo.CreateAsync(careAboutModel);

            if (careAboutModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]

        public async Task<IActionResult> DeleteCareAbout(string documentName)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            // Kiểm tra nếu appUser là null
            if (appUser == null)
            {
                return NotFound("User not found.");
            }

            var userCareAbout = await _careAboutRepo.GetUserCareAbout(appUser);

            var filteredDocument = userCareAbout
                .Where(s => s.DocummentName.ToLower() == documentName.ToLower())
                .ToList();

            if (filteredDocument.Count == 1)
            {
                await _careAboutRepo.DeleteCareAbout(appUser, documentName);
            }
            else
            {
                return BadRequest("Document not in your care about.");
            }

            return Ok();
        }


    }
}