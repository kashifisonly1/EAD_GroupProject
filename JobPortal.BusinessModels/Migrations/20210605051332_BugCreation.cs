﻿using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPortal.BusinessModels.Migrations
{
	public partial class BugCreation : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<string>(nullable: false),
					Name = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<string>(nullable: false),
					UserName = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
					Email = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(nullable: false),
					PasswordHash = table.Column<string>(nullable: true),
					SecurityStamp = table.Column<string>(nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					PhoneNumber = table.Column<string>(nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(nullable: false),
					TwoFactorEnabled = table.Column<bool>(nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
					LockoutEnabled = table.Column<bool>(nullable: false),
					AccessFailedCount = table.Column<int>(nullable: false),
					FullName = table.Column<string>(nullable: true),
					ProfileImage = table.Column<string>(nullable: true),
					DateCreated = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Catogories",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(nullable: true),
					Slug = table.Column<string>(nullable: true),
					ImageLink = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Catogories", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ContactMessages",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					MessageDate = table.Column<DateTime>(nullable: false),
					Email = table.Column<string>(nullable: true),
					Subject = table.Column<string>(nullable: true),
					Message = table.Column<string>(nullable: true),
					Name = table.Column<string>(nullable: true),
					IsResponded = table.Column<bool>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ContactMessages", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleId = table.Column<string>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<string>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetUserClaims_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(nullable: false),
					ProviderKey = table.Column<string>(nullable: false),
					ProviderDisplayName = table.Column<string>(nullable: true),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_AspNetUserLogins_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserId = table.Column<string>(nullable: false),
					RoleId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserId = table.Column<string>(nullable: false),
					LoginProvider = table.Column<string>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Value = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_AspNetUserTokens_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Freelancers",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<string>(nullable: true),
					Detail = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Freelancers", x => x.Id);
					table.ForeignKey(
						name: "FK_Freelancers_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "CustomOrderRequests",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ClientId = table.Column<string>(nullable: true),
					Title = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true),
					Duration = table.Column<int>(nullable: false),
					Budget = table.Column<double>(nullable: false),
					RequestDate = table.Column<DateTime>(nullable: false),
					CategoryId = table.Column<int>(nullable: true),
					ImageUrl = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CustomOrderRequests", x => x.Id);
					table.ForeignKey(
						name: "FK_CustomOrderRequests_Catogories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Catogories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_CustomOrderRequests_AspNetUsers_ClientId",
						column: x => x.ClientId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Gigs",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true),
					Pricing = table.Column<decimal>(nullable: false),
					PriceUnit = table.Column<string>(nullable: true),
					ImageUrl = table.Column<string>(nullable: true),
					CategoryId = table.Column<int>(nullable: true),
					FreelancerId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Gigs", x => x.Id);
					table.ForeignKey(
						name: "FK_Gigs_Catogories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Catogories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Gigs_Freelancers_FreelancerId",
						column: x => x.FreelancerId,
						principalTable: "Freelancers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Skills",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(nullable: true),
					FreelancerId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Skills", x => x.Id);
					table.ForeignKey(
						name: "FK_Skills_Freelancers_FreelancerId",
						column: x => x.FreelancerId,
						principalTable: "Freelancers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Orders",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Details = table.Column<string>(nullable: true),
					Status = table.Column<string>(nullable: true),
					StartDate = table.Column<DateTime>(nullable: false),
					EndDate = table.Column<DateTime>(nullable: false),
					FreelancerId = table.Column<string>(nullable: true),
					ClientId = table.Column<string>(nullable: true),
					GigId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Orders", x => x.Id);
					table.ForeignKey(
						name: "FK_Orders_AspNetUsers_ClientId",
						column: x => x.ClientId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction,
						onUpdate: ReferentialAction.NoAction);
					table.ForeignKey(
						name: "FK_Orders_AspNetUsers_FreelancerId",
						column: x => x.FreelancerId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Orders_Gigs_GigId",
						column: x => x.GigId,
						principalTable: "Gigs",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction,
						onUpdate: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateTable(
				name: "OrdersDelivery",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					OrderId = table.Column<int>(nullable: true),
					Details = table.Column<string>(nullable: true),
					FileUrl = table.Column<string>(nullable: true),
					DeliveryDate = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OrdersDelivery", x => x.Id);
					table.ForeignKey(
						name: "FK_OrdersDelivery_Orders_OrderId",
						column: x => x.OrderId,
						principalTable: "Orders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_AspNetRoleClaims_RoleId",
				table: "AspNetRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserId",
				table: "AspNetUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserLogins_UserId",
				table: "AspNetUserLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_RoleId",
				table: "AspNetUserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_CustomOrderRequests_CategoryId",
				table: "CustomOrderRequests",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_CustomOrderRequests_ClientId",
				table: "CustomOrderRequests",
				column: "ClientId");

			migrationBuilder.CreateIndex(
				name: "IX_Freelancers_UserId",
				table: "Freelancers",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Gigs_CategoryId",
				table: "Gigs",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Gigs_FreelancerId",
				table: "Gigs",
				column: "FreelancerId");

			migrationBuilder.CreateIndex(
				name: "IX_Orders_ClientId",
				table: "Orders",
				column: "ClientId");

			migrationBuilder.CreateIndex(
				name: "IX_Orders_FreelancerId",
				table: "Orders",
				column: "FreelancerId");

			migrationBuilder.CreateIndex(
				name: "IX_Orders_GigId",
				table: "Orders",
				column: "GigId");

			migrationBuilder.CreateIndex(
				name: "IX_OrdersDelivery_OrderId",
				table: "OrdersDelivery",
				column: "OrderId");

			migrationBuilder.CreateIndex(
				name: "IX_Skills_FreelancerId",
				table: "Skills",
				column: "FreelancerId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserLogins");

			migrationBuilder.DropTable(
				name: "AspNetUserRoles");

			migrationBuilder.DropTable(
				name: "AspNetUserTokens");

			migrationBuilder.DropTable(
				name: "ContactMessages");

			migrationBuilder.DropTable(
				name: "CustomOrderRequests");

			migrationBuilder.DropTable(
				name: "OrdersDelivery");

			migrationBuilder.DropTable(
				name: "Skills");

			migrationBuilder.DropTable(
				name: "AspNetRoles");

			migrationBuilder.DropTable(
				name: "Orders");

			migrationBuilder.DropTable(
				name: "Gigs");

			migrationBuilder.DropTable(
				name: "Catogories");

			migrationBuilder.DropTable(
				name: "Freelancers");

			migrationBuilder.DropTable(
				name: "AspNetUsers");
		}
	}
}
