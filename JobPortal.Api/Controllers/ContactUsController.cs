using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JobPortal.BusinessModels;
using JobPortal.DataManager.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Api.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ContactUsController : ControllerBase
	{
		[Authorize(Roles = ("Client, Freelancer"))]
		[Route("SendMessage")]
		[HttpPost]
		public async Task SendMessageAsync(ContactUsModel model)
		{
			ContactUsData data = new ContactUsData();
			await data.SaveMessageAsync(model);
		}

		[Authorize(Roles = ("Admin"))]
		[Route("Delete")]
		[HttpPost]
		public void DeleteMessage(int id)
		{
			ContactUsData data = new ContactUsData();
			data.DeleteMessage(id);
		}

		[Authorize(Roles = ("Admin"))]
		[Route("Resolve")]
		[HttpPost]
		public void ResolveMessage(int id)
		{
			ContactUsData data = new ContactUsData();
			data.MarkMessageAsResolved(id);
		}

		[Authorize(Roles = ("Admin"))]
		[Route("Unresolved")]
		[HttpGet]
		public List<ContactUsModel> GetUnresolvedMessages()
		{
			ContactUsData data = new ContactUsData();
			return data.GetAllUnResolvedMessages();
		}
	}
}
