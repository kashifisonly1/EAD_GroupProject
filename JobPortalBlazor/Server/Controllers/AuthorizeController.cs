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
				UserName = parameters.UserName,
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
				UserName = parameters.UserName,
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
		public async Task<AspNetUser> CurrentUser()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			await Task.Delay(3000);
			var roles = await _userManager.GetRolesAsync(user);
			return user;
		}

		[HttpGet]
		public UserInfo UserInfo()
		{
			//var user = await _userManager.GetUserAsync(HttpContext.User);
			return BuildUserInfo();
		}

		private UserInfo BuildUserInfo()
		{
			return new UserInfo
			{
				IsAuthenticated = User.Identity.IsAuthenticated,
				UserName = User.Identity.Name,
				ExposedClaims = User.Claims
					//Optionally: filter the claims you want to expose to the client
					//.Where(c => c.Type == "test-claim")
					.ToDictionary(c => c.Type, c => c.Value)
			};
		}
	}
}
