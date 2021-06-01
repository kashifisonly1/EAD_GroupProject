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


		[Column(TypeName = "datetime2(7)")]
		public DateTime DateCreated { get; set; } = DateTime.Now;

		[PersonalData]
		[Column(TypeName = "nvarchar(max)")]
		public string ProfileImage { get; set; }

	}
}
