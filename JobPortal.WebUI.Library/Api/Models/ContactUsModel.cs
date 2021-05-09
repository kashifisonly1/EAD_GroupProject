using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobPortal.WebUI.Library.Api.Models
{
	public class ContactUsModel
	{
		public int Id { get; set; }

		public DateTime MessageDate { get; set; }

		public string Email { get; set; }

		public string Subject { get; set; }

		public string Message { get; set; }

		public string Name { get; set; }

		public bool IsResponded { get; set; }

	}
}
