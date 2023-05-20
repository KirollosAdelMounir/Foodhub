using Eatable.Repository;
using Eatable.Models;
using Eatable.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eatable.Areas.Identity.Data;

namespace Eatable.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepositoryBase<Order, long> _orderRepository;
        private readonly IRepositoryBase<FoodItem, long> _foodItemRepository;
        private readonly IRepositoryBase<Restaurant, long> _restaurantRepository;
        private readonly IRepositoryBase<ApplicationUser, string> _userRepository;
        public AdminController(
            IRepositoryBase<Order, long> orderRepository,
            IRepositoryBase<FoodItem,long> foodItemRepository,
            IRepositoryBase<Restaurant, long> restaurantRepository,
            IRepositoryBase<ApplicationUser, string> userRepository
            )
        {
            _orderRepository = orderRepository;
            _foodItemRepository = foodItemRepository;
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
        }
        // GET: AdminController
        public ActionResult Index() => RedirectToAction("Dashboard");
        public ActionResult Dashboard()
        {
            IEnumerable<Order> _orders = _orderRepository.List();
            IEnumerable<FoodItem> _foodItems = _foodItemRepository.List();
            IEnumerable<Restaurant> _restaurants = _restaurantRepository.List();
            IEnumerable<ApplicationUser> _users = _userRepository.List();
            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                orders = _orders,
                restaurants = _restaurants,
                foodItems = _foodItems,
                applicationUsers = _users
            };
            return View(dashboardViewModel);
        }

    }
}
