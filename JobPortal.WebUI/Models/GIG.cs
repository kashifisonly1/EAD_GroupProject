using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Models
{
    public class GIG:PageModel
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        public string Category { get; set; }

        public string Skill { get; set; }

        public decimal Pricing { get; set; }
        public string Keywords { get; set; }

    }
}
