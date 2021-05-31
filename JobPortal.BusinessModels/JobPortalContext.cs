using JobPortal.BusinessModels.Client;
using JobPortal.BusinessModels.Freelancers;
using JobPortal.BusinessModels.General;
using JobPortal.BusinessModels.Gigs;
using JobPortal.BusinessModels.Orders;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels
{
	public class JobPortalContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Freelancer> Freelancers { get; set; }
		public DbSet<Skill> Skills { get; set; }

		public DbSet<Category> Catogories { get; set; }

		public DbSet<Gig> Gigs { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderDelivery> OrdersDelivery { get; set; }

		public DbSet<ContactUsModel> ContactMessages { get; set; }


		public DbSet<CustomOrderRequest> CustomOrderRequests { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JobPortalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

		public static JobPortalContext Create()
		{
			return new JobPortalContext();
		}
	}
}
