using FoodHub.Repository;
using FoodHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodHub.ViewModels;

namespace FoodHub.Controllers
{
    public class FoodItemController : Controller
    {
        private readonly IRepositoryBase<FoodItem, long> _foodItemRepository;
        private readonly IRepositoryBase<Restaurant, long> _restaurantRepository;
        public FoodItemController(IRepositoryBase<FoodItem, long> foodItemRepository, IRepositoryBase<Restaurant, long> restaurantRepository)
        {
            _foodItemRepository = foodItemRepository;
            _restaurantRepository = restaurantRepository;
        }
        // GET: FoodItemController
        public ActionResult Index()
        {
            return View(_foodItemRepository.Find(x => x.RestaurantId > 0, false, x => x.Restaurant));
        }

        // GET: FoodItemController/Create
        public ActionResult Create()
        {
            FoodItemCreateViewModel foodItemCreateViewModel = new FoodItemCreateViewModel()
            {
                restaurants = _restaurantRepository.List().ToList()
            };
            return View(foodItemCreateViewModel);
        }

        // POST: FoodItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodItem foodItem)
        {
            try
            {
                _foodItemRepository.Create(foodItem);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FoodItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_foodItemRepository.GetById(id));
        }

        // POST: FoodItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FoodItem foodItem)
        {
            try
            {
                _foodItemRepository.Update(foodItem);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FoodItemController/Delete/5
        public ActionResult Delete(int id)
        {
            var foodItem = _foodItemRepository.GetById(id);
            return View(foodItem);
        }

        // POST: FoodItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FoodItem foodItem)
        {
            try
            {
                _foodItemRepository.Delete(foodItem);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
