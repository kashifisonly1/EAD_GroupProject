using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Models
{
    public class PurchaseRequestForm : PageModel
    {
        public int RequestID { get; set; }
        public String UserID{get;set;}
        public User user { get; set; }

        [Required]
        public string RequestSubject { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string RequestDescription { get; set; }
        [Required]
        public int RequestDuration { get; set; } // will be in days 
        [Required]
        public IFormFile Image { get; set; }
        public String ImageUrl { get; set; }
        [Required]
        public decimal RequestBudget { get; set; }
        
        // need to add category after confirming as either string or category class object
        public int RequestCategoryID { get; set; } // should be single category for simplicity. Purpose is for Search 
        public Category category { get; set; }
    }
}
