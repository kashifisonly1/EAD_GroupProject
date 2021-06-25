using JobPortalBlazor.Shared;

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UploadController : ControllerBase
	{
		private readonly JobPortalDBContext _context;

		public UploadController(JobPortalDBContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<String>> Index(IFormFile file)
		{
			String file_ext = Path.GetExtension(file.FileName);
			String unique_name = Guid.NewGuid() + file_ext;
			String filepath = Path.Combine("wwwroot", "img");
			filepath = Path.Combine("client", filepath);
			filepath = Path.Combine("..", filepath);
			String fileurl = Path.Combine(filepath, unique_name);
			try
			{
				FileStream fileStream = new FileStream(fileurl, FileMode.Create);
				Stream rd = file.OpenReadStream();
				await rd.CopyToAsync(fileStream);
				fileStream.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return unique_name;
		}
	}
}
