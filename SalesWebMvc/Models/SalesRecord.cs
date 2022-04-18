using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }   
        public DateTime Date { get; set; } 
        public decimal Amount { get; set; }
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; }


        public SalesRecord()
        {

        }

        public SalesRecord(int id, DateTime date, decimal amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
