using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Models
{
    public class User
    {
        public String UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        public String ImageUrl { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public string RoleName { get; set; }
        [Required]
        public IBrowserFile Image { get; set; }
    }
}
