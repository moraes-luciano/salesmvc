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

        public IEnumerable<Department> FindAll()
        {
            return _dbContext.Department.OrderBy(obj=>obj.Name);
        }
    }
}
