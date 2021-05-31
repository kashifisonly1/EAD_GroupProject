using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity;

namespace JobPortal.BusinessModels.General
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }

		public string ProfileImage { get; set; }

		public DateTime DateCreated { get; set; }

	}
}
