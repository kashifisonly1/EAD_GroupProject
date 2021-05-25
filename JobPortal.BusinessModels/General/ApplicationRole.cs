using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.General
{
	public class ApplicationRole
	{
		public string Id { get; set; }

		public string ConcurrencyStamp { get; set; }

		public string Name { get; set; }

		public string NormalizedName { get; set; }

	}
}
