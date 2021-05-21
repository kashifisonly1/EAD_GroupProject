using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace JobPortal.WebUI.Models
{
    public class User : PageModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }

        public string UserEmail { get; set; }
        public string RoleName { get; set; }
        public IFormFile Image { get; set; }
        public bool isAdmin { get; set; }

    }
}
