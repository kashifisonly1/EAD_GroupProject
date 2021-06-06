using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Models
{
    public class Gig
    {
        public int ID { get; set; }
        public String UserID { get; set; }
        public User user { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public string CategoryID { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public String PriceInterval { get; set; }
        [Required]
        public IBrowserFile Image { get; set; }
        public String ImageUrl { get; set; }
    }
}
