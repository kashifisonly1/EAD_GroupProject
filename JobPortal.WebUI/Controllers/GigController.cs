using JobPortal.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Controllers
{
    public class GigController : Controller
    {
        public IActionResult Index(String id)
        {
            GIG gig = new GIG { Title = "Web Programming", Description = "Lorem ipsum dolor sit amet, consectetuer ad", Pricing = 5, PriceUnit = "Daily", ImageUrl = "banner.jpg", user = new Models.User { UserName = "kashif tariq", ImageUrl = "banner.jpg" } };
            ViewData["Title"] = id;
            ViewData["gig"] = gig;
            return View("View");
        }
    }
}
