using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.Client
{
	public class CustomOrderRequest
	{
		public int Id { get; set; } // PK

		public string ClientId { get; set; } // FK-User

		public string Title { get; set; }

		public string Description { get; set; }

		public int Offers { get; set; }

		public int Duration { get; set; }

		public double Budget { get; set; }

		public DateTime RequestDate { get; set; }

		public int CategoryId { get; set; } // FK-Category

	}
}
