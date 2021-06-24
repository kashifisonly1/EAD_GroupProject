using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Models
{
	public class User
	{
		public String UserID { get; set; }
		//[Required]
		public string UserName { get; set; }
		public String ImageUrl { get; set; }
		//[Required]
		public string UserEmail { get; set; }
		public string RoleName { get; set; }
		//[Required]
		public IBrowserFile Image { get; set; }

		public User() { }
		public User(JobPortalBlazor.Shared.AspNetUser u)
		{
			UserID = u.Id;
			UserEmail = u.Email;
			UserName = u.FullName;
			ImageUrl = u.ProfileImage;
		}
		public static implicit operator JobPortalBlazor.Shared.AspNetUser(User u)
		{
			JobPortalBlazor.Shared.AspNetUser a = new JobPortalBlazor.Shared.AspNetUser();
			a.Id = u.UserID;
			a.FullName = u.UserName;
			a.Email = u.UserEmail;
			a.ProfileImage = u.ImageUrl;
			return a;
		}
	}
}
