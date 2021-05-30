using JobPortal.BusinessModels.Freelancers;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.Gigs
{
	public class Gig
	{
		public int Id { get; set; } // PK
		public string Title { get; set; }
		public string Description { get; set; }

		public decimal Pricing { get; set; }
		public String PriceUnit { get; set; }
		public string ImageUrl { get; set; }

		public Category CategoryID { get; set; } // FK-Category

		public Freelancer FreelancerID { get; set; } // FK-User

	}
}
