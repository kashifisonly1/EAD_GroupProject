using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortalBlazor.Shared
{
	public partial class OrderDelivery
	{
		public int Id { get; set; }
		public int? OrderId { get; set; }
		public string Details { get; set; }
		public string FileUrl { get; set; }
		public DateTime DeliveryDate { get; set; }

		public virtual Order Order { get; set; }
	}
}
