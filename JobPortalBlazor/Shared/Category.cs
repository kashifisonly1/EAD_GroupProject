using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortalBlazor.Shared
{
	public class Category
	{
		public Category()
		{
			CustomOrderRequests = new HashSet<CustomOrderRequest>();
			Gigs = new HashSet<Gig>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public string ImageLink { get; set; }

		public virtual ICollection<CustomOrderRequest> CustomOrderRequests { get; set; }
		public virtual ICollection<Gig> Gigs { get; set; }
	}
}
