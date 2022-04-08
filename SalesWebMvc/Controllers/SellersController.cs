using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerlService, DepartmentService departmentService)
        {
            _sellerService = sellerlService;
            _departmentService = departmentService;


        }
        public IActionResult Index()
        {
            IEnumerable<Seller> sellers = _sellerService.FindAll();
            return View(sellers);
        }
        public IActionResult Create()
        {
            IEnumerable<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) 
        {
            _sellerService.Add(seller);
            return RedirectToAction("Index");
        
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error",new { message = "Id not provided."});
            }
            Seller seller = _sellerService.FindById(id.Value);
            if(seller == null)
            {
                return RedirectToAction("Error", new { message = "Id not found." });
            }
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Error", new { message = "Id not provided." });
            }
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction("Error", new { message = "Id not found." });
            }
            
            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", new { message = "Id not provided." });
            }
            Seller seller =_sellerService.FindById(id.Value);
            if(seller == null)
            {
                return RedirectToAction("Error", new { message = "Id not found." });
            }
            IEnumerable<Department> departments = _departmentService.FindAll();
            SellerFormViewModel sfm = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(sfm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if(id!= seller.Id)
             {
                return RedirectToAction("Error", new { message = "Id mismatch." });
            }
            try
            {
                _sellerService.Edit(seller);
                return RedirectToAction("Index");
            }
            catch (ApplicationException e)
            {
                return RedirectToAction("Error", new { message = e.Message });
            }
            
            
        }

        public IActionResult Error(string message)
        {
            ErrorViewModel errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message };
            return View(errorModel);
        }
    }
}
