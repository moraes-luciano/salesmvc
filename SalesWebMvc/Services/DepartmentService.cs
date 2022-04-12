using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly AppDbContext _dbContext;

        public DepartmentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Department>> FindAllAsync()
        {
            return await _dbContext.Department.OrderBy(obj=>obj.Name).ToListAsync();
        }
    }
}
