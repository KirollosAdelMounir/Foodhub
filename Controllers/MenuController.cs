﻿using FoodHub.Areas.Identity.Data;
using FoodHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers
{
    public class MenuController : Controller
    {
        List<FoodItem> items = new List<FoodItem>
            {
                new FoodItem
                {
                    FoodItemId = 1,
                    FoodItemName = "Dinner Box 2",
                    Price = (float)250.00,
                    RestaurantId = 1
                },
                new FoodItem
                {
                    FoodItemId = 2,
                    FoodItemName = "Dinner Box 3",
                    Price = (float)325.00,
                    RestaurantId = 1
                },
                new FoodItem
                {
                    FoodItemId = 3,
                    FoodItemName = "Dinner Box 4",
                    Price = (float)570.00,
                    RestaurantId = 1
                },
                new FoodItem
                {
                    FoodItemId = 4,
                    FoodItemName = "Dinner Box 5",
                    Price = (float)840.00,
                    RestaurantId = 1
                }
            };
        public IActionResult Index() => View(items);
        public IActionResult Details(int id)
        {
            var item = items.Find(x => x.FoodItemId == id);
            return View(item);
        }
    }
}