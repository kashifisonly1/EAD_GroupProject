using JobPortal.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Controllers
{
    public class OfferController : Controller
    {
        public IActionResult Index(String id)
        {
            PurchaseRequestForm req = new PurchaseRequestForm { RequestID = 1, RequestDescription = "I need a Logo designed by a Vue!", RequestDuration = 2, RequestBudget = 5, RequestSubject = "Title", RequestCategoryID = 1, category = new Category { Name = "category" }, UserID = "123", user = new User { UserName = "kashif tariq", ImageUrl = "banner.jpg" }, ImageUrl = "banner.jpg" };
            ViewData["Title"] = id;
            ViewData["offer"] = req;
            return View();
        }
    }
}
