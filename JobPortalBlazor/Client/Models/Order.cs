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

        public Order() { }
        public Order(JobPortalBlazor.Shared.Order o)
        {
            ID = o.Id;
            FreelancerID = o.Gig.Freelancer.User.Id;
            ClientID = o.Client.Id;
            Details = o.Details;
            Status = o.Status;
            StartDate = o.StartDate;
            EndDate = o.EndDate;
            Freelancer = new User(o.Gig.Freelancer.User);
            Client = new User(o.Client);
            GigID = o.Gig.Id;
            this.Gig = new Gig(o.Gig);
        }
        public static implicit operator JobPortalBlazor.Shared.Order(Order o)
        {
            JobPortalBlazor.Shared.Order or = new JobPortalBlazor.Shared.Order();
            or.Id = o.ID;
            or.ClientId = o.ClientID;
            or.Details = o.Details;
            or.EndDate = o.EndDate;
            or.StartDate = o.StartDate;
            or.Status = o.Status;
            or.GigId = o.GigID;
            return or;
        }
    }
}
