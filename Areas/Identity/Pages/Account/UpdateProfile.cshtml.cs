using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobPortal.WebUI.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class UpdateProfileModel : PageModel
	{
		[BindProperty]
		public InputModel Input { get; set; }
		public string ReturnUrl { get; private set; }
		public object ExternalLogins { get; private set; }

		public class InputModel
		{
			[Required]
			[Display(Name = "Upload Your Image")]
			public IFormFile Image { get; set; }

			[Required]
			[Display(Name = "Full Name")]
			public string FullName { get; set; }

			[Required]
			[Display(Name = "Last Name")]
			public string LastName { get; set; }

			// TODO -- Unknown input field
			[Required]
			[Display(Name = "Birthday")]
			public DateTime Birthday { get; set; }


			[Required]
			[Display(Name = "Gender")]
			public string Gender { get; set; }

			// TODO -- Unknown input field
			[Required]
			[Display(Name = "Phone Number")]
			public Decimal Phone { get; set; }

			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; }

			[Required]
			[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }
		}

		public void OnGet(string returnUrl = null)
		{
			ReturnUrl = returnUrl;

		}

		public void OnPost(string returnUrl = null)
		{
			ReturnUrl = returnUrl;

		}
	}
}
