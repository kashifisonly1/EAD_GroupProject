using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using Microsoft.AspNetCore.Components.Forms;

namespace JobPortalBlazor.Shared
{
	public class RegisterParameters
	{
		public string UserID { get; set; }
		[Required]
		public string UserName { get; set; }
		public string ImageUrl { get; set; }
		[Required]
		public string UserEmail { get; set; }

		//public string RoleName { get; set; }
		//[Required]
		//public IBrowserFile Image { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
