using JobPortalBlazor.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthorizeController : ControllerBase
	{
		private readonly UserManager<AspNetUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<AspNetUser> _signInManager;

		public AuthorizeController(UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginParameters parameters)
		{
			var user = await _userManager.FindByEmailAsync(parameters.UserName);
			if (user == null) return BadRequest("User does not exist");
			var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
			if (!singInResult.Succeeded) return BadRequest("Invalid password");

			await _signInManager.SignInAsync(user, parameters.RememberMe);

			return Ok("Logged In Successfully.");
		}


		[HttpPost]
		public async Task<IActionResult> Register(RegisterParameters parameters)
		{
			string[] roles = { "Admin", "Freelancer", "Client" };
			foreach (var item in roles)
			{
				if (!await _roleManager.RoleExistsAsync(item))
					await _roleManager.CreateAsync(new IdentityRole(item));
			}

			var user = new AspNetUser
			{
				UserName = parameters.UserEmail,
				ProfileImage = parameters.ImageUrl,
				Email = parameters.UserEmail,
				FullName = parameters.UserName
			};
			var result = await _userManager.CreateAsync(user, parameters.Password);
			if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

			user = await _userManager.FindByEmailAsync(user.Email);

			if (!_userManager.Users.Any())
				await _userManager.AddToRoleAsync(user, "Admin");
			else
				await _userManager.AddToRoleAsync(user, "Client");

			return await Login(new LoginParameters
			{
				UserName = parameters.UserEmail,
				Password = parameters.Password
			});
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<JobPortalBlazor.Client.Models.User>> CurrentUser()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			if (user == null)
				return new JobPortalBlazor.Client.Models.User();
			var roles = await _userManager.GetRolesAsync(user);
			JobPortalBlazor.Client.Models.User user_data = new JobPortalBlazor.Client.Models.User { 
				UserID=user.Id,
				UserName=user.FullName,
				UserEmail=user.Email,
				ImageUrl=user.ProfileImage,
				RoleName="Client"
			};
			foreach (string s in roles)
				if (s != "Client")
					user_data.RoleName = s;
			return user_data;
		}

		[HttpGet]
		public UserInfo UserInfo()
		{
			//var user = await _userManager.GetUserAsync(HttpContext.User);
			return BuildUserInfo();
		}

		private UserInfo BuildUserInfo()
		{
			var x = User.Claims.ToList();
			Dictionary<string, string> claims = new Dictionary<string, string>();
			foreach(var a in x)
            {
				string existed_val = "";
				if(claims.TryGetValue(a.Type, out existed_val)==false || (existed_val=="Client"))
					claims[a.Type] = a.Value;
            }
			return new UserInfo
			{
				IsAuthenticated = User.Identity.IsAuthenticated,
				UserName = User.Identity.Name,
				ExposedClaims = claims
			};
		}
	}
}
