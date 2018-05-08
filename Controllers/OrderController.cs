using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models.ViewModels;
using BookCave.Repositories;
using BookCave.Models;
//using BookCave.Services

namespace BookCave.Controllers
{
    public class OrderController : Controller
    {
        public OrderRepo _orderServices; 
        public OrderController()
        {
            _orderServices = new OrderRepo();
        }
        public IActionResult Index() //Displays all orders for a logged in user
        { 
            var allOrderFromusers = _orderServices.GetByOwnerId();
            
            return View(allOrderFromusers.ToList());
        }

        public IActionResult Details(int? id)
        { 
            if(id == null)
            {
                return View("Error");
            }
            var orders = _orderServices.GetById(id);
            if(orders == null)
            {
                return View("NotFound");
            }
            return View(orders);
        }

        [HttpDelete] //veit ekki hvort þetta eigi að vera hér
        public IActionResult Delete(int ISBN)
        {
            //Þarf að útfæra betur, nota Ajax
            return View();
        }
        [HttpGet]
        public IActionResult Buy(OrderViewModel bookTobuy)
        {
            //Þarf að útfæra betur
            var buybooks = _orderServices.Buy(bookTobuy);
            return View();
        }
<<<<<<< HEAD
       /* [HttpPost]
=======
        /*[HttpPost]
>>>>>>> 1b3e75ba30f922011ea4e06c2575913dbddb616f
        public IActionResult AddToCart(int bookAdded)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            _orderServices.AddToCart(bookAdded);
            return RedirectToAction("Cart");
        }
        public IActionResult Cart()
        {
            var booksInCart = _orderServices.Cart();
            //er bara að skoða körfu
            return View();
        }
<<<<<<< HEAD
        */

=======

*/
>>>>>>> 1b3e75ba30f922011ea4e06c2575913dbddb616f
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}