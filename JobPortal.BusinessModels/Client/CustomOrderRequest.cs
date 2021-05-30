using JobPortal.BusinessModels.General;
using JobPortal.BusinessModels.Gigs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.Client
{
	public class CustomOrderRequest
	{
		public int Id { get; set; } // PK

		public ApplicationUser ClientID { get; set; } // FK-User

		public string Title { get; set; }

		public string Description { get; set; }

		public int Duration { get; set; }

		public double Budget { get; set; }

		public DateTime RequestDate { get; set; }

		public Category CategoryId { get; set; } // FK-Category
		public string ImageUrl { get; set; }
	}
}
