using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Models
{
    public class ContactForm : PageModel
    {
        public int ID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
