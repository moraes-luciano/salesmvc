using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
   
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {

            return View();
        }

        public async Task <IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            //because DateTime? is a nullable
            if (!minDate.HasValue)
            {
                minDate = new DateTime(2022, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

           
         
            List<SalesRecord> salesRecords = await _salesRecordService.FindByDateAsync(minDate.Value, maxDate.Value);
            return View(salesRecords);
        }

       
    }
}
