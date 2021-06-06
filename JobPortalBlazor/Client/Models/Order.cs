using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Models
{
    public class Order
    {
        public int ID;
        [Required]
        public String FreelancerID;
        [Required]
        public String ClientID;
        [Required]
        [DataType(DataType.MultilineText)]
        public String Details;
        public String Status;
        public DateTime StartDate;
        public DateTime EndDate;
        public User Freelancer;
        public User Client;
        public int GigID;
        public Gig Gig;
        public int BidID;
        public PurchaseRequest Bid;
    }
}
