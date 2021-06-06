using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace JobPortalBlazor.Shared
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }

		public string ProfileImage { get; set; }

		public DateTime DateCreated { get; set; }
	}
}
