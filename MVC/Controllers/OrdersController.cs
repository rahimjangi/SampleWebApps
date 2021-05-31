using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public OrdersController(IFoodData foodData,IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var food = await _foodData.GetFood();
            OrderCreateModel model = new OrderCreateModel();
            food.ForEach(item => {
                model.FoodItems.Add(new SelectListItem { 
                    Value=item.Id.ToString(),
                    Text=item.Title
                });
            });

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderModel order)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            var food = await _foodData.GetFood();
            order.Total = order.Quantity * food.Where(x => x.Id == order.FoodId).First().Price;
            int id = await _orderData.CreateOrder(order);
            return RedirectToAction("Display",new { id});
        }

        public async Task<IActionResult> Display(int id)
        {
            OrderDisplayModel displayModel = new OrderDisplayModel();
            displayModel.Order = await _orderData.GetOrderById(id);
            if (displayModel.Order!=null)
            {
                var food = await _foodData.GetFood();
                displayModel.ItemPurchased = food.Where(x => x.Id == displayModel.Order.FoodId).FirstOrDefault()?.Title;
               
            }
            return View(displayModel);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, string orderName)
        {
            await _orderData.UpdateOrderName(id, orderName);
            return RedirectToAction("Display", new { id});
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order =await _orderData.GetOrderById(id);
            return View(order);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(OrderModel order)
        {
            await _orderData.DeleteOrder(order.Id);
            return RedirectToAction("Create");

        }
    }
}
