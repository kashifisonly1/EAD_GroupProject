using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace JobPortal.WebUI.Models
{
    public class Category : PageModel
    {
        public int ID { get; set; }
        public int PID {get; set;}
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description{ get; set; }
        
        public IFormFile Image { get; set; }
    }
}
