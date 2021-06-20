using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortalBlazor.Shared
{
	public partial class CustomOrderRequest
	{
		public int Id { get; set; }
		public string ClientId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Duration { get; set; }
		public double Budget { get; set; }
		public DateTime RequestDate { get; set; }
		public int? CategoryId { get; set; }
		public string ImageUrl { get; set; }

		public virtual Category Category { get; set; }
		public virtual AspNetUser Client { get; set; }
	}
}
