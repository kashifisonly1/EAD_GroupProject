using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalBlazor.Shared
{
	public class SupportMessage
	{
		public int Id { get; set; }

		public ApplicationUser User { get; set; }

		public DateTime MessageDate { get; set; }

		public string Subject { get; set; }

		public string Message { get; set; }

		public bool IsResponded { get; set; }
	}
}
