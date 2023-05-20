using Eatable.Areas.Identity.Data;
using Eatable.Models;
using Eatable.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eatable.Controllers
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
