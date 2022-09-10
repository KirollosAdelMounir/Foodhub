using FoodHub.Areas.Identity.Data;
using FoodHub.Models;
using FoodHub.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers
{
    public class MenuController : Controller
    {
        private readonly IRepositoryBase<FoodItem, long> _repository;
        public MenuController(IRepositoryBase<FoodItem, long> repository) => _repository = repository;
        public IActionResult Index() => View(_repository.List().ToList());
        
        //public IActionResult Details(int id)
        //{
        //    var item = items.Find(x => x.FoodItemId == id);
        //    return View(item);
        //}
    }
}
