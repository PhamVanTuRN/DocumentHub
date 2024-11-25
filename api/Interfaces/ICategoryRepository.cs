using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> ExistsAsync(int categoryId);
    }
}