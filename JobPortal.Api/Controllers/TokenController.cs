﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using JobPortal.Api.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JobPortal.Api.Controllers
{
	public class TokenController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;

		public TokenController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		[Route("/token")]
		[HttpPost]
		public async Task<IActionResult> Create(string username, string password, string grant_type)
		{
			if (await IsValidUsernameAndPassword(username, password))
			{
				return new ObjectResult(await GenerateToken(username));
			}
			else
			{
				return BadRequest();
			}
		}

		private async Task<bool> IsValidUsernameAndPassword(string username, string password)
		{
			var user = await _userManager.FindByEmailAsync(username);
			return await _userManager.CheckPasswordAsync(user, password);
		}

		private async Task<dynamic> GenerateToken(string username)
		{
			var user = await _userManager.FindByEmailAsync(username);
			var roles = from ur in _context.UserRoles
						join r in _context.Roles on ur.RoleId equals r.Id
						where ur.UserId == user.Id
						select new { ur.UserId, ur.RoleId, r.Name };


			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, username),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
				new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(10)).ToUnixTimeSeconds().ToString()) // HACK -- Update Token properties (expiration time)
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, username));
			}

			var token = new JwtSecurityToken(
				new JwtHeader(new SigningCredentials(
					new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OurSecretKey")),
					SecurityAlgorithms.HmacSha256)),
				new JwtPayload(claims)
				);

			var output = new
			{
				Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
				Username = username
				// HACK -- Add other fields to token (if needed)
			};

			return output;
		}
	}
}