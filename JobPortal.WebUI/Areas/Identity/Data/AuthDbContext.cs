﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

using JobPortal.WebUI.Areas.Identity.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.WebUI.Data
{
	public class AuthDbContext : IdentityDbContext<ApplicationUser>
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
			//AspNetUsers -> User
			builder.Entity<ApplicationUser>()
				.ToTable("User");
			//AspNetRoles -> Role
			builder.Entity<IdentityRole>()
				.ToTable("Role");
			//AspNetUserRoles -> UserRole
			builder.Entity<IdentityUserRole>()
				.ToTable("UserRole");
			//AspNetUserClaims -> UserClaim
			builder.Entity<IdentityUserClaim>()
				.ToTable("UserClaim");
			//AspNetUserLogins -> UserLogin
			builder.Entity<IdentityUserLogin>()
				.ToTable("UserLogin");
		}
	}
}
