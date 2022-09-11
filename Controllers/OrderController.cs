using FoodHub.Areas.Identity.Data;
using FoodHub.Models;
using FoodHub.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;


namespace FoodHub.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        private readonly IRepositoryBase<Order,long> _order_repository;
        private readonly IRepositoryBase<FoodItem, long> _foodItem_repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public OrderController(IRepositoryBase<Order, long> Order_repository, IRepositoryBase<FoodItem, long> FoodItem_repository,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _order_repository = Order_repository;
            _foodItem_repository = FoodItem_repository;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public ActionResult Cart()
        {
            var validorders = _order_repository.List().Where(order => order.Quantity > 0).ToList();
            var fooditems = _foodItem_repository.List().ToList();
            validorders.ForEach(item => item.fooditem = fooditems.FirstOrDefault(food => food.FoodItemId == item.fooditemId));
            return View(validorders);
        }

        // GET: OrderController/Details/5
        [HttpGet]
        public ActionResult AddNewItem(int id)
        {
            ApplicationUser user = null;
            Order order1;

            if (_signInManager.IsSignedIn(User))
            {
                user = _userManager.GetUserAsync(User).Result;
            }
            else
            {
                TempData["SignInError"] = "Sign in First";
                return RedirectToAction("Index", "Menu");
            }
            if ((order1 = _order_repository.List().SingleOrDefault(ord => ord.fooditemId == id)) != null)
            {
                order1.Quantity++;
                _order_repository.Update(order1);

                return RedirectToAction("Cart");
            }
            Order order = new() { UserId = user.Id, fooditemId = id, Quantity = 1, OrderDate = DateTime.Now };
            _order_repository.Create(order);

            return RedirectToAction("Cart");
        }

        // GET: OrderController/Create
        [HttpPost]
        public ActionResult AddItem(int id)
        {
            var order = _order_repository.List().SingleOrDefault(ord => ord.fooditemId == id);
            order.Quantity++;
            _order_repository.Update(order);

            order = _order_repository.Find(ord => ord.fooditemId == order.fooditemId, false, ord => ord.fooditem).ToList()[0]; 

            return Json(order);
        }

        // POST: OrderController/Create
        [HttpPost]
        public ActionResult RemoveItem(int id)
        {
            var order = _order_repository.List().SingleOrDefault(ord => ord.fooditemId == id);
            order.Quantity--;
            _order_repository.Update(order);

            order = _order_repository.Find(ord => ord.fooditemId == order.fooditemId, false, ord => ord.fooditem).ToList()[0];
            //var validorders = _order_repository.Find(order => order.Quantity > 0, false, order => order.fooditem, order => order.user).ToList();
            return Json(order);
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Cart");
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
