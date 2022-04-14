using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly AppDbContext _dbContext;

        public SalesRecordService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SalesRecord>>FindByDateAsync(DateTime minDate, DateTime maxDate)
        {
 
            IQueryable<SalesRecord> result = _dbContext.SalesRecord.Where(x => x.Date >= minDate && x.Date <= maxDate)
                                                 .Include(x => x.Seller)
                                                 .Include(x => x.Seller.Department)
                                                 .OrderByDescending(x => x.Date);

            
            
            List<SalesRecord> salesRecords = await result.ToListAsync();
            return (salesRecords);
        }
    }
}
