using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.Gigs
{
	public class Gig
	{
		public int Id { get; set; } // PK

		public string Description { get; set; }

		public decimal Pricing { get; set; }

		public string Keywords { get; set; }

		public int CategoryId { get; set; } // FK-Category

		public string FreelancerId { get; set; } // FK-User

	}
}
