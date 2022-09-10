using FoodHub.Repository;
using FoodHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRepositoryBase<Restaurant, long> _repository;
        public RestaurantController(IRepositoryBase<Restaurant, long> repository) => _repository = repository;
        // GET: RestaurantController
        public ActionResult Index() => View(_repository.List());

        // GET: RestaurantController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestaurantController/Create
       [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant)
        {
            try
            {
                _repository.Create(restaurant);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RestaurantController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_repository.GetById(id));
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Restaurant restaurant)
        {
            try
            {
                _repository.Update(restaurant);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RestaurantController/Delete/5
        public ActionResult Delete(int id)
        {
            var restaurant = _repository.GetById(id);
            return View(restaurant);
        }

        // POST: RestaurantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Restaurant restaurant)
        {
            try
            {
                _repository.Delete(restaurant);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
