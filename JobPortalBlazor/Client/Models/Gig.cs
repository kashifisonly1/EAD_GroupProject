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
        public int freelancerID { get; set; }
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
        public Gig() { }
        public Gig(JobPortalBlazor.Shared.Gig gig)
        {
            this.ID = gig.Id;
            this.CategoryID = gig.Category.Id.ToString();
            this.ImageUrl = gig.ImageUrl;
            this.Price = gig.Pricing;
            this.PriceInterval = gig.PriceUnit;
            this.UserID = gig.Freelancer.User.Id;
            this.user = new User(gig.Freelancer.User);
            this.Description = gig.Description;
            this.Title = gig.Title;
            this.freelancerID = gig.Freelancer.Id;
        }
        public static implicit operator JobPortalBlazor.Shared.Gig(Gig gig)
        {
            JobPortalBlazor.Shared.Gig t = new JobPortalBlazor.Shared.Gig();
            t.Id = gig.ID;
            t.CategoryId = int.Parse(gig.CategoryID);
            t.PriceUnit = gig.PriceInterval;
            t.Pricing = gig.Price;
            t.ImageUrl = gig.ImageUrl;
            t.Description = gig.Description;
            t.Title = gig.Title;
            t.FreelancerId = gig.freelancerID;
            return t;
        }

    }
}
