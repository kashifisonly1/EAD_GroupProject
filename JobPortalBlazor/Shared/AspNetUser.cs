using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

#nullable disable

namespace JobPortalBlazor.Shared
{
	public partial class AspNetUser : IdentityUser
	{
		public AspNetUser()
		{
			AspNetUserClaims = new HashSet<AspNetUserClaim>();
			AspNetUserLogins = new HashSet<AspNetUserLogin>();
			AspNetUserRoles = new HashSet<AspNetUserRole>();
			AspNetUserTokens = new HashSet<AspNetUserToken>();
			CustomOrderRequests = new HashSet<CustomOrderRequest>();
			Freelancers = new HashSet<Freelancer>();
			Orders = new HashSet<Order>();
			SupportMessages = new HashSet<SupportMessage>();
		}

		//public string Id { get; set; }
		public string FullName { get; set; }
		public string ProfileImage { get; set; }
		public DateTime DateCreated { get; set; }
		//public string UserName { get; set; }
		//public string NormalizedUserName { get; set; }
		//public string Email { get; set; }
		//public string NormalizedEmail { get; set; }
		//public bool EmailConfirmed { get; set; }
		//public string PasswordHash { get; set; }
		//public string SecurityStamp { get; set; }
		//public string ConcurrencyStamp { get; set; }
		//public string PhoneNumber { get; set; }
		//public bool PhoneNumberConfirmed { get; set; }
		//public bool TwoFactorEnabled { get; set; }
		//public DateTimeOffset? LockoutEnd { get; set; }
		//public bool LockoutEnabled { get; set; }
		//public int AccessFailedCount { get; set; }

		public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
		public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
		public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
		public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
		public virtual ICollection<CustomOrderRequest> CustomOrderRequests { get; set; }
		public virtual ICollection<Freelancer> Freelancers { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<SupportMessage> SupportMessages { get; set; }
	}
}
