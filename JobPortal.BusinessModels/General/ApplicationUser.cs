using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.General
{
	public class ApplicationUser
	{
		public string Id { get; set; }

		public int AccessFailedCount { get; set; }

		public string ConcurrencyStamp { get; set; }

		public DateTime DateCreated { get; set; }

		public string Email { get; set; }

		public bool EmailConfirmed { get; set; }

		public string FullName { get; set; }

		public bool LockoutEnabled { get; set; }

		public DateTime LocoutEnd { get; set; }

		public string NormalizedEmail { get; set; }

		public string NormalizedUsername { get; set; }

		public string PasswordHash { get; set; }

		public string PhoneNumber { get; set; }

		public bool PhoneNumberConfirmed { get; set; }

		public bool SecurityStamp { get; set; }

		public string Username { get; set; }

		public string ProfileImage { get; set; }

	}
}
