using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace JobPortalBlazor.Shared
{
	public partial class JobPortalDBContext : DbContext
	{
		public JobPortalDBContext()
		{
		}

		public JobPortalDBContext(DbContextOptions<JobPortalDBContext> options)
			: base(options)
		{
		}

		public virtual DbSet<AspNetRole> Roles { get; set; }
		public virtual DbSet<AspNetRoleClaim> RoleClaims { get; set; }
		public virtual DbSet<AspNetUser> Users { get; set; }
		public virtual DbSet<AspNetUserClaim> UserClaims { get; set; }
		public virtual DbSet<AspNetUserLogin> UserLogins { get; set; }
		public virtual DbSet<AspNetUserRole> UserRoles { get; set; }
		public virtual DbSet<AspNetUserToken> UserTokens { get; set; }
		public virtual DbSet<Category> Catogories { get; set; }
		public virtual DbSet<CustomOrderRequest> CustomOrderRequests { get; set; }
		public virtual DbSet<Freelancer> Freelancers { get; set; }
		public virtual DbSet<Gig> Gigs { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderDelivery> OrderDeliveries { get; set; }
		public virtual DbSet<Skill> Skills { get; set; }
		public virtual DbSet<SupportMessage> SupportMessages { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
				optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JobPortal-DB;Integrated Security=True;");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

			modelBuilder.Entity<AspNetRole>(entity =>
			{
				entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
					.IsUnique()
					.HasFilter("([NormalizedName] IS NOT NULL)");

				entity.Property(e => e.Name).HasMaxLength(256);

				entity.Property(e => e.NormalizedName).HasMaxLength(256);
			});

			modelBuilder.Entity<AspNetRoleClaim>(entity =>
			{
				entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

				entity.Property(e => e.RoleId).IsRequired();

				entity.HasOne(d => d.Role)
					.WithMany(p => p.AspNetRoleClaims)
					.HasForeignKey(d => d.RoleId);
			});

			modelBuilder.Entity<AspNetUser>(entity =>
			{
				entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

				entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
					.IsUnique()
					.HasFilter("([NormalizedUserName] IS NOT NULL)");

				entity.Property(e => e.Email).HasMaxLength(256);

				entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

				entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

				entity.Property(e => e.UserName).HasMaxLength(256);
			});

			modelBuilder.Entity<AspNetUserClaim>(entity =>
			{
				entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

				entity.Property(e => e.UserId).IsRequired();

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserClaims)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<AspNetUserLogin>(entity =>
			{
				entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

				entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

				entity.Property(e => e.UserId).IsRequired();

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserLogins)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<AspNetUserRole>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.RoleId });

				entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

				entity.HasOne(d => d.Role)
					.WithMany(p => p.AspNetUserRoles)
					.HasForeignKey(d => d.RoleId);

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserRoles)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<AspNetUserToken>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserTokens)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<CustomOrderRequest>(entity =>
			{
				entity.HasIndex(e => e.CategoryId, "IX_CustomOrderRequests_CategoryId");

				entity.HasIndex(e => e.ClientId, "IX_CustomOrderRequests_ClientId");

				entity.HasOne(d => d.Category)
					.WithMany(p => p.CustomOrderRequests)
					.HasForeignKey(d => d.CategoryId);

				entity.HasOne(d => d.Client)
					.WithMany(p => p.CustomOrderRequests)
					.HasForeignKey(d => d.ClientId);
			});

			modelBuilder.Entity<Freelancer>(entity =>
			{
				entity.HasIndex(e => e.UserId, "IX_Freelancers_UserId");

				entity.HasOne(d => d.User)
					.WithMany(p => p.Freelancers)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<Gig>(entity =>
			{
				entity.HasIndex(e => e.CategoryId, "IX_Gigs_CategoryId");

				entity.HasIndex(e => e.FreelancerId, "IX_Gigs_FreelancerId");

				entity.Property(e => e.Pricing).HasColumnType("decimal(18, 2)");

				entity.HasOne(d => d.Category)
					.WithMany(p => p.Gigs)
					.HasForeignKey(d => d.CategoryId);

				entity.HasOne(d => d.Freelancer)
					.WithMany(p => p.Gigs)
					.HasForeignKey(d => d.FreelancerId);
			});

			modelBuilder.Entity<Order>(entity =>
			{
				entity.HasIndex(e => e.ClientId, "IX_Orders_ClientId");

				entity.HasIndex(e => e.GigId, "IX_Orders_GigId");

				entity.HasOne(d => d.Client)
					.WithMany(p => p.Orders)
					.HasForeignKey(d => d.ClientId);

				entity.HasOne(d => d.Gig)
					.WithMany(p => p.Orders)
					.HasForeignKey(d => d.GigId);
			});

			modelBuilder.Entity<OrderDelivery>(entity =>
			{
				entity.HasIndex(e => e.OrderId, "IX_OrderDeliveries_OrderId");

				entity.HasOne(d => d.Order)
					.WithMany(p => p.OrderDeliveries)
					.HasForeignKey(d => d.OrderId);
			});

			modelBuilder.Entity<Skill>(entity =>
			{
				entity.HasIndex(e => e.FreelancerId, "IX_Skills_FreelancerId");

				entity.HasOne(d => d.Freelancer)
					.WithMany(p => p.Skills)
					.HasForeignKey(d => d.FreelancerId);
			});

			modelBuilder.Entity<SupportMessage>(entity =>
			{
				entity.HasIndex(e => e.UserId, "IX_SupportMessages_UserId");

				entity.HasOne(d => d.User)
					.WithMany(p => p.SupportMessages)
					.HasForeignKey(d => d.UserId);
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
