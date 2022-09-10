using FoodHub.Repository;
using FoodHub.Models;
using FoodHub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepositoryBase<Order, long> _orderRepository;
        private readonly IRepositoryBase<FoodItem, long> _foodItemRepository;
        private readonly IRepositoryBase<Restaurant, long> _restaurantRepository;
        public AdminController(
            IRepositoryBase<Order, long> orderRepository, IRepositoryBase<FoodItem, long> foodItemRepository, IRepositoryBase<Restaurant, long> restaurantRepository
            )
        {
            _orderRepository = orderRepository;
            _foodItemRepository = foodItemRepository;
            _restaurantRepository = restaurantRepository;
        }
        // GET: AdminController
        public ActionResult Index() => RedirectToAction("Dashboard");
        public ActionResult Dashboard()
        {
            IEnumerable<Order> _orders = _orderRepository.List();
            IEnumerable<FoodItem> _foodItems = _foodItemRepository.List();
            IEnumerable<Restaurant> _restaurants = _restaurantRepository.List();
            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                orders = _orders,
                restaurants = _restaurants,
                foodItems = _foodItems
            };
            return View(dashboardViewModel);
        }

    }
}
