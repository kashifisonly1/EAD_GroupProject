using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortalBlazor.Shared
{
	public partial class SupportMessage
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime MessageDate { get; set; }
		public string Subject { get; set; }
		public string Message { get; set; }
		public bool IsResponded { get; set; }

		public virtual AspNetUser User { get; set; }
	}
}
