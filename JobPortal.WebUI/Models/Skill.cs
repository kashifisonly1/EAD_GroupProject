using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace JobPortal.WebUI.Models
{
    public class Skill : PageModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
