using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;  // Đảm bảo bạn có ApplicationDbContext

        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int categoryId)
        {
            // Kiểm tra xem CategoryId có tồn tại trong cơ sở dữ liệu không
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }

        // Implement các phương thức khác nếu cần
    }

}