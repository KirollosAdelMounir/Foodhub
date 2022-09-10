using FoodHub.Repository;
using FoodHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers
{
    public class FoodItemController : Controller
    {
        private readonly IRepositoryBase<FoodItem, long> _repository;
        public FoodItemController(IRepositoryBase<FoodItem, long> repository) => _repository = repository;
        // GET: FoodItemController
        public ActionResult Index() => View(_repository.List());

        // GET: FoodItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodItem foodItem)
        {
            try
            {
                _repository.Create(foodItem);
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
            return View(_repository.GetById(id));
        }

        // POST: FoodItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FoodItem foodItem)
        {
            try
            {
                _repository.Update(foodItem);
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
            var foodItem = _repository.GetById(id);
            return View(foodItem);
        }

        // POST: FoodItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FoodItem foodItem)
        {
            try
            {
                _repository.Delete(foodItem);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
