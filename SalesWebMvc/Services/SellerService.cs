
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
        public IEnumerable<Seller> FindAll()
        {
            return _dbContext.Seller.OrderBy(obj=>obj.Id);
        }

        public void Add(Seller seller)
        {
            _dbContext.Add(seller);
            _dbContext.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _dbContext.Seller.Include(obj=>obj.Department).FirstOrDefault(obj=>obj.Id == id);
        }

        public void Delete(int id)
        {
            Seller seller = _dbContext.Seller.Find(id);
            if(seller != null)
            {
                _dbContext.Seller.Remove(FindById(id));
                _dbContext.SaveChanges();
            }
           
        }

        public void Edit(Seller seller) 
        { 
            if (!_dbContext.Seller.Any(obj=> obj.Id == seller.Id))
            {
                throw new NotFoundException("Id not found.");
            }
            try
            {
                _dbContext.Seller.Update(seller);
                _dbContext.SaveChanges();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
