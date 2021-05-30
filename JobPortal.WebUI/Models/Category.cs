using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace JobPortal.WebUI.Models
{
    public class Category : PageModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        public IFormFile Image { get; set; }
        public String ImageUrl{get;set;}
    }
}
