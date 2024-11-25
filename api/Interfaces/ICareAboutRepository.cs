using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICareAboutRepository
    {
        Task<List<Document>> GetUserCareAbout(AppUser user);
        Task<CareAbout> CreateAsync(CareAbout careAbout);
        Task<CareAbout> DeleteCareAbout(AppUser appUser, string documentName);
    }
}