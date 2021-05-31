using JobPortal.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult ViewOrder()
        {
            Order dummyOrder = new Order();
            dummyOrder.ID = 1;
            dummyOrder.StartDate = DateTime.Now;
            dummyOrder.Status = "Running";
            dummyOrder.Details = "This is details of the order, yes more details, yes more more details";
            dummyOrder.ClientID = "1";
            dummyOrder.FreelancerID = "2";
            dummyOrder.GigID = 2;
            dummyOrder.freelancer = new User { UserName = "kashif" };
            dummyOrder.client = new User { UserName = "atif" };
            dummyOrder.gig = new GIG { Title = "This is Gig Title" };
            List<OrderData> orderDatas = new List<OrderData>();
            OrderData orderData = new OrderData { Details="Here is your Order", FileURL="link"};
            orderDatas.Add(orderData);
            orderDatas.Add(orderData);
            ViewData["Order"] = dummyOrder;
            ViewData["OrderFiles"] = orderDatas;
            return View();
        }
    }
}
