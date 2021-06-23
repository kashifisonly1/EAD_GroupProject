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

        public PurchaseRequest() { }

        public PurchaseRequest(JobPortalBlazor.Shared.CustomOrderRequest r)
        {
            RequestID = r.Id;
            UserID = r.Client.Id;
            user = new User(r.Client);
            Subject = r.Title;
            Description = r.Description;
            PriceInterval = r.Duration;
            ImageUrl = r.ImageUrl;
            Price = (decimal)r.Budget;
            CategoryID = r.Category.Id;
            category = new Category(r.Category);
        }
        public static implicit operator JobPortalBlazor.Shared.CustomOrderRequest(PurchaseRequest p)
        {
            JobPortalBlazor.Shared.CustomOrderRequest r = new JobPortalBlazor.Shared.CustomOrderRequest();
            r.Id = p.RequestID;
            r.CategoryId = p.CategoryID;
            r.Budget = (double)p.Price;
            r.Duration = p.PriceInterval;
            r.ClientId = p.UserID;
            r.Title = p.Subject;
            r.Description = p.Description;
            r.ImageUrl = p.ImageUrl;
            return r;
        }
    }
}
