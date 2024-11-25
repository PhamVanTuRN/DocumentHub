using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : ICareAboutRepository
    {
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<CareAbout> CreateAsync(CareAbout careAbout)
        {
            await _context.CareAbout.AddAsync(careAbout);
            await _context.SaveChangesAsync();
            return careAbout;
        }

        public async Task<CareAbout> DeleteCareAbout(AppUser appUser, string documentName)
        {
            var careAboutModel = await _context.CareAbout.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Document.DocummentName.ToLower() == documentName.ToLower());

            if (careAboutModel == null)
            {
                return null;
            }

            _context.CareAbout.Remove(careAboutModel);
            await _context.SaveChangesAsync();
            return careAboutModel;
        }

        public async Task<List<Document>> GetUserCareAbout(AppUser user)
        {
            return await _context.CareAbout.Where(u => u.AppUserId == user.Id)
            .Select(document => new Document
            {
                Id = document.DocumentId,
                DocummentName = document.Document.DocummentName,
                Summary = document.Document.Summary,

                DocumentUrl = document.Document.DocumentUrl,

            }).ToListAsync();
        }
    }
}