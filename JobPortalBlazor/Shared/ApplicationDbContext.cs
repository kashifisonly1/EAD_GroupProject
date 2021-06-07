using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobPortalBlazor.Shared
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Freelancer> Freelancers { get; set; }

		public DbSet<Skill> Skills { get; set; }

		public DbSet<Category> Catogories { get; set; }

		public DbSet<Gig> Gigs { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderDelivery> OrdersDelivery { get; set; }

		public DbSet<SupportMessage> SupportMessages { get; set; }


		public DbSet<CustomOrderRequest> CustomOrderRequests { get; set; }

		// HACK -- Update Connection string
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JobPortal-DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}
