using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Models
{
    public class OrderDelivery
    {
        public int ID { get; set; }
        public Order order { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public String Details { get; set; }
        public String FileURL { get; set; }
        [Required]
        public IBrowserFile File { get; set; }
        public DateTime Date;

    }
}
