using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalBlazor.Shared
{
	public class Gig
	{
		public int Id { get; set; } // PK

		public string Title { get; set; }

		public string Description { get; set; }

		public decimal Pricing { get; set; }

		public string PriceUnit { get; set; }

		public string ImageUrl { get; set; }

		public Category Category { get; set; } // FK-Category

		public Freelancer Freelancer { get; set; } // FK-User

	}
}
