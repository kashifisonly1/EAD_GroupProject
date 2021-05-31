using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPortal.WebUI.Models;
using System.Diagnostics;

namespace JobPortal.WebUI.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult RequestForm()
        {
            return View("PurchaseRequest");
        }
        public IActionResult MyOrders()
        {
            List<Order> orders = new List<Order>();
            Order dummyOrder = new Order();
            dummyOrder.ID = 1;
            dummyOrder.StartDate = DateTime.Now;
            dummyOrder.Status = "Running";
            dummyOrder.ClientID = "1";
            dummyOrder.FreelancerID = "2";
            dummyOrder.GigID = 2;
            dummyOrder.freelancer = new User { UserName = "kashif" };
            dummyOrder.client = new User { UserName = "atif" };
            dummyOrder.gig = new GIG { Title = "This is Gig Title" };
            orders.Add(dummyOrder);
            ViewData["Order-List"] = orders;
            return View();
        }

        public IActionResult PurchaseRequestCard() 
        {

            //TODO:  handle PurchaseRequestForm data to store in database
            List<PurchaseRequestForm> list = new List<PurchaseRequestForm>();
            for(int i=0; i<4;i++)
            {
                PurchaseRequestForm req = new PurchaseRequestForm { RequestID = 1, RequestDescription = "I need a Logo designed by a Vue!", RequestDuration = 2, RequestBudget = 5 };
                list.Add(req);

            }
            ViewData["request-view-card"] = list;
            return View("PurchaseRequestCard");

        }

        public void RequestHandler()
        {

            //TODO:  handler for bid invites  

            Console.WriteLine("Handle Stuff");
        }


        public IActionResult PurchaseRequest()
        {
            //TODO:  handle PurchaseRequestForm data to store in database
            List<PurchaseRequestForm> list = new List<PurchaseRequestForm>();
           PurchaseRequestForm  req = new PurchaseRequestForm { RequestID = 1, RequestDescription = "I need a Logo designed by a Vue!", RequestDuration = 2, RequestBudget = 5};
            list.Add(req);

            ViewData["purchase-request"] = list;
            return View("PurchaseRequestView");
            
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
