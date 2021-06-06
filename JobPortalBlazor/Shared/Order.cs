using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalBlazor.Shared
{
	public class Order
	{
		public int Id { get; set; } // PK

		public string Details { get; set; }

		public string Status { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public ApplicationUser Client { get; set; } // FK-User

		public Gig Gig { get; set; } // FK-Gig

	}
}
