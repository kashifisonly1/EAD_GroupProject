using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.Orders
{
	public class OrderDelivery
	{
		public int Id { get; set; } // PK

		public Order OrderId { get; set; } // FK-Order

		public string Details { get; set; }

		public string FileUrl { get; set; }

		public DateTime DeliveryDate { get; set; }

	}
}
