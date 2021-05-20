using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace JobPortal.WebUI.Areas.Identity.Data
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser
	{
		[Required]
		[PersonalData]
		[Column(TypeName = "NVARCHAR(50)")]
		public string FullName { get; set; }

		[Required]
		[PersonalData]
		[Column(TypeName = "NVARCHAR(50)")]
		public string Role { get; set; }

		[Column(TypeName = "datetime2(7)")]
		public DateTime DateCreated { get; set; } = DateTime.Now;

		[Required]
		[PersonalData]
		[Column(TypeName = "varbinary(max)")]
		public byte[] ProfileImage { get; set; }

	}
}
