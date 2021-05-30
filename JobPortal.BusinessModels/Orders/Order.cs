using JobPortal.BusinessModels.Freelancers;
using JobPortal.BusinessModels.General;
using JobPortal.BusinessModels.Gigs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.Orders
{
	public class Order
	{
		public int Id { get; set; } // PK

		public string Details { get; set; }

		public string Status { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public Freelancer FreelancerId { get; set; } // FK-User

		public ApplicationUser ClientId { get; set; } // FK-User

		public Gig GigId { get; set; } // FK-Gig

	}
}
