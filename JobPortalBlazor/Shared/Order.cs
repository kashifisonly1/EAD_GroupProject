using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortalBlazor.Shared
{
	public partial class Order
	{
		public Order()
		{
			OrderDeliveries = new HashSet<OrderDelivery>();
		}

		public int Id { get; set; }
		public string Details { get; set; }
		public string Status { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string ClientId { get; set; }
		public int? GigId { get; set; }

		public virtual AspNetUser Client { get; set; }
		public virtual Gig Gig { get; set; }
		public virtual ICollection<OrderDelivery> OrderDeliveries { get; set; }
	}
}
