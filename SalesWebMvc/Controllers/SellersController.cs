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
        public async Task<IActionResult> Index()
        {
            IEnumerable<Seller> sellers = await _sellerService.FindAllAsync();
            return View(sellers);
        }
        public async Task<IActionResult> Create()
        {
            IEnumerable<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller) 
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Department> departments = await _departmentService.FindAllAsync();
                SellerFormViewModel sfvm = new SellerFormViewModel{ Seller = seller, Departments = departments };
                return View(sfvm);
            }  
            await _sellerService.AddAsync(seller);
            return RedirectToAction("Index");
        
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error",new { message = "Id not provided."});
            }
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if(seller == null)
            {
                return RedirectToAction("Error", new { message = "Id not found." });
            }
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Error", new { message = "Id not provided." });
            }
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null)
            {
                return RedirectToAction("Error", new { message = "Id not found." });
            }
            
            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Error", new { message = "Id not provided." });
            }

            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if(seller == null)
            {
                return RedirectToAction("Error", new { message = "Id not found." });
            }
            IEnumerable<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel sfm = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(sfm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Department> departments = await _departmentService.FindAllAsync();
                SellerFormViewModel sfvm = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(sfvm);
            }
            if (id!= seller.Id)
             {
                return RedirectToAction("Error", new { message = "Id mismatch." });
            }
            try
            {
                await _sellerService.EditAsync(seller);
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
