using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortalBlazor.Shared
{
	public partial class Gig
	{
		public Gig()
		{
			Orders = new HashSet<Order>();
		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Pricing { get; set; }
		public string PriceUnit { get; set; }
		public string ImageUrl { get; set; }
		public int? CategoryId { get; set; }
		public int? FreelancerId { get; set; }

		public virtual Category Category { get; set; }
		public virtual Freelancer Freelancer { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
	}
}
