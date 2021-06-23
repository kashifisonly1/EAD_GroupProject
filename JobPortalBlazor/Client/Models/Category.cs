using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required]
        public IBrowserFile Image { get; set; }
        public String ImageUrl { get; set; }

        public Category() {}
        public Category(JobPortalBlazor.Shared.Category cat)
        {
            this.ID = cat.Id;
            this.Name = cat.Name;
            this.ImageUrl = cat.ImageLink;
            this.Slug = cat.Slug;
        }
        public static implicit operator JobPortalBlazor.Shared.Category (Category cat)
        {
            JobPortalBlazor.Shared.Category c = new JobPortalBlazor.Shared.Category();
            c.Id = cat.ID;
            c.ImageLink = cat.ImageUrl;
            c.Name = cat.Name;
            c.Slug = cat.Slug;
        }
    }
}
