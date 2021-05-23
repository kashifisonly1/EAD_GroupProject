﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using static JobPortal.WebUI.Models.Freelancer;
using JobPortal.WebUI.Models;

namespace JobPortal.WebUI.Controllers
{
    public class Freelancer : Controller
    {

        private readonly ILogger<Freelancer> _logger;

        public Freelancer(ILogger<Freelancer> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult BecomeFreelancer()
        {
            List<Skill> skillList = new List<Skill>();
            skillList.Add(new Skill { Name = "Web Programming" });
            skillList.Add(new Skill { Name = "Data Entry" });
            skillList.Add(new Skill { Name = "Data Base" });
            skillList.Add(new Skill { Name = "Web Designing" });
            skillList.Add(new Skill { Name = "Desktop Application" });
            ViewData["Skill-List"] = skillList;

            
            return View();
        }
        //[HttpGet]
        //public IActionResult BecomeFreelancer()
        //{
        //    return View();
        //}

        public IActionResult Gigs()
        {
            List<GIG> giglist = new List<GIG>();
            giglist.Add(new GIG {Title="Web Programming", Description = "Lorem ipsum dolor sit amet, consectetuer ad", Pricing=5});
            giglist.Add(new GIG{Title="Desktop Application",  Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit", Pricing=10});
            giglist.Add(new GIG {Title="Mobile Computing", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.",Pricing=20});
            ViewData["Gig-List"] = giglist;
            return View("MyGigs");
        }
    }
}