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
        
        [Required]
        public string RequestNamer { get; set; } // can fetch from login info

        public string RequestSubject { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string RequestDescription { get; set; }
        
        public int RequestOfferCount { get; set; }
        
        [Required]
        public int RequestDuration { get; set; } // will be in days 
        
        [Required]
        public double RequestBudget { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }  // currently giving though form but you can add date and time when submit is done(for more simplicity)

        // need to add category after confirming as either string or category class object
        public string RequestCategory { get; set; } // should be single category for simplicity. Purpose is for Search 

        
    }
}
