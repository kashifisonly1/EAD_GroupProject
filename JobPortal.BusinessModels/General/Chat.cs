using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.General
{
	public class Chat
	{
		public int Id { get; set; } // PK

		public string Message { get; set; }

		public string SenderId { get; set; } // FK-User

		public string ReceiverId { get; set; } // FK-User

		public DateTime MessageTime { get; set; }

	}
}
