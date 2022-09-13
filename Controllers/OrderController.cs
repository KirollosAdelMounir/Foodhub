using FoodHub.Areas.Identity.Data;
using FoodHub.Models;
using FoodHub.Repository;
using FoodHub.ViewModels;
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
            var user = _userManager.GetUserAsync(User).Result;
            var validorders = _order_repository.List().Where(order => order.State == State.Pending && order.UserId == user.Id).ToList();
            var fooditems = _foodItem_repository.List().ToList();
            validorders.ForEach(item => item.fooditem = fooditems.FirstOrDefault(food => food.FoodItemId == item.fooditemId));
            UserOrders userOrders = new() { orders = validorders ,userid= user.Id };
            return View(userOrders);
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
            if ((order1 = _order_repository.List().SingleOrDefault(ord => ord.fooditemId == id && ord.UserId == user.Id && ord.State == State.Pending)) != null)
            {
                order1.Quantity++;
                _order_repository.Update(order1);

                return RedirectToAction("Cart");
            }
            Order order = new() { UserId = user.Id, fooditemId = id, Quantity = 1, OrderDate = DateTime.Now ,State = State.Pending};
            _order_repository.Create(order);

            return RedirectToAction("Cart");
        }

        // GET: OrderController/Create
        [HttpPost]
        public ActionResult AddItem(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var order = _order_repository.List().SingleOrDefault(ord => ord.Id == id);
            order.Quantity++;
            _order_repository.Update(order);

            order = _order_repository.Find(ord => ord.Id == order.Id, false, ord => ord.fooditem).ToList()[0]; 

            return Json(order);
        }

        // POST: OrderController/Create
        [HttpPost]
        public ActionResult RemoveItem(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var order = _order_repository.List().SingleOrDefault(ord => ord.Id == id);
            order.Quantity--;
            if (order.Quantity == 0)
            {
                _order_repository.Delete(order);
            }
            else {
                _order_repository.Update(order);
                order = _order_repository.Find(ord => ord.Id == order.Id, false, ord => ord.fooditem).ToList()[0];
            }
            //var validorders = _order_repository.Find(order => order.Quantity > 0, false, order => order.fooditem, order => order.user).ToList();
            return Json(order);
        }
        [HttpPost]
        public JsonResult ReturnAllRelatedOrders(string id) {
            var orders = _order_repository.Find(ord => ord.UserId == id && ord.State == State.Pending,false,ord=>ord.fooditem).ToList();

            return Json(orders);
        }
        [HttpGet]
        public ActionResult CancelOrders(string id)
        {
            var orders = _order_repository.List().Where(ord => ord.UserId == id && ord.State == State.Pending).ToList();
            orders.ForEach(item => {
                item.State = State.Cancelled;
                _order_repository.Update(item);
            });
            return RedirectToAction("Cart");
        }
        [HttpGet]
        public ActionResult ApproveOrders(string id)
        {
            var orders = _order_repository.List().Where(ord => ord.UserId == id && ord.State == State.Pending).ToList();
            orders.ForEach(item => {
                item.State = State.Delivered;
                _order_repository.Update(item);
            });
            return RedirectToAction("Cart");
        }

        public ActionResult ViewAllOrders() {
            var allorders = _order_repository.Find(order => order.Quantity>0,false,order=>order.fooditem,Order=>Order.user);
            return View(allorders);
        }
    }
}
