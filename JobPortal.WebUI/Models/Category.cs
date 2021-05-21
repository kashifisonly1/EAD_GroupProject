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
        [Required]
        public int ID { get; set; }
        [Required]
        public int PID {get; set;}
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description{ get; set; }
        
        public IFormFile Image { get; set; }
    }
}
