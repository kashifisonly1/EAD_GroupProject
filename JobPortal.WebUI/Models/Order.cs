using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Models
{
    public class Order
    {
        public int ID;
        public String FreelancerID;
        public String ClientID;
        public String Details;
        public String Status;
        public int GigID;
        public DateTime StartDate;
        public DateTime EndDate;
        public User freelancer;
        public User client;
        public GIG gig;
    }
}
