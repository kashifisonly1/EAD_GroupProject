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
        
        public OrderDelivery() { }

        public OrderDelivery(JobPortalBlazor.Shared.OrderDelivery o)
        {
            ID = o.Id;
            OrderID = (int)o.OrderId;
            this.order = o.Order==null? null:new Order(o.Order);
            Details = o.Details;
            FileURL = o.FileUrl;
            Date = o.DeliveryDate;
        }
        public static implicit operator JobPortalBlazor.Shared.OrderDelivery(OrderDelivery o)
        {
            JobPortalBlazor.Shared.OrderDelivery d = new JobPortalBlazor.Shared.OrderDelivery();
            d.Id = o.ID;
            d.OrderId = o.OrderID;
            d.Details = o.Details;
            d.FileUrl = o.FileURL;
            d.DeliveryDate = o.Date;
            return d;
        }
    }
}
