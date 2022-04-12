
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly AppDbContext _dbContext;

        public SellerService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Seller>> FindAllAsync()
        {
            return await _dbContext.Seller.OrderBy(obj => obj.Id).ToListAsync();
        }

        public async Task AddAsync(Seller seller)
        {
            _dbContext.Add(seller);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _dbContext.Seller.Include(obj=>obj.Department).FirstOrDefaultAsync(obj=>obj.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            Seller seller = await _dbContext.Seller.FindAsync(id);
            if(seller != null)
            {
                _dbContext.Seller.Remove(seller);
                await _dbContext.SaveChangesAsync();
            }
           
        }

        public async Task EditAsync(Seller seller) 
        {
            bool hasAny = await _dbContext.Seller.AnyAsync(obj => obj.Id == seller.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found.");
            }
            try
            {
                _dbContext.Seller.Update(seller);
                await _dbContext.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
