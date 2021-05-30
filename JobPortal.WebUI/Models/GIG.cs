using Microsoft.AspNetCore.Http;
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
        public String ID { get; set; }
        public String UserID { get; set; }
        public User user { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public decimal Pricing { get; set; }
        [Required]
        public String PriceUnit { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public String ImageUrl { get; set; }


    }
}
