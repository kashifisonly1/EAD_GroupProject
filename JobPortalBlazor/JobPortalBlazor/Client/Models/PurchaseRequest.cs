using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Models
{
    public class PurchaseRequest
    {
        public int RequestID { get; set; }
        public String UserID { get; set; }
        public User user { get; set; }

        [Required]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public int PriceInterval { get; set; }
        [Required]
        public IBrowserFile Image { get; set; }
        public String ImageUrl { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public Category category { get; set; }
    }
}
