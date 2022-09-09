using FoodHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        public static List<Order> orders = new() { new Order { customerId = 1, foodItemId = 1, OrderDate = new DateTime(2022, 9, 9, 12, 0, 0), Quantity = 1 } };
        public static List<Customer> customers = new() { new Customer { ID = 1, Name = "Gamal", IsVisible = true } };

        public ActionResult Cart()
        {
            var validorders = orders.Where(order => order.Quantity > 0).ToList();
            validorders.ForEach(item => item.foodItem = MenuController.items.FirstOrDefault(food => food.FoodItemId == item.foodItemId));
            return View(validorders);
        }

        // GET: OrderController/Details/5
        [HttpGet]
        public ActionResult AddNewItem(int id)
        {
            if (orders.SingleOrDefault(ord => ord.foodItemId == id) != null)
            {
                orders.ForEach(ord => { if (ord.foodItemId == id) ord.Quantity++; });
                return RedirectToAction("Cart");
            }
            Order order = new() { customerId = 1, foodItemId = id, Quantity = 1, OrderDate = new DateTime().Date };
            orders.Add(order);

            return RedirectToAction("Cart");
        }

        // GET: OrderController/Create
        [HttpPost]
        public ActionResult AddItem(int id)
        {
            orders.ForEach(ord => { if (ord.foodItemId == id) ord.Quantity++; });
            var validorders = orders.Where(order => order.Quantity > 0).ToList();
            return Json(validorders);
        }

        // POST: OrderController/Create
        [HttpPost]
        public ActionResult RemoveItem(int id)
        {
            orders.ForEach(ord => { if (ord.foodItemId == id) ord.Quantity--; });
            var validorders = orders.Where(order => order.Quantity > 0).ToList();
            return Json(validorders);
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
