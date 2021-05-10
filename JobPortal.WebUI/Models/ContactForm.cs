using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Models
{
<<<<<<< HEAD
	public class ContactForm
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
=======
    public class ContactForm
    {
        public int ID {get;set;}
        [Required]
        [EmailAddress]
        public string Email { get; set; }
>>>>>>> master

		[Required]
		public string Subject { get; set; }
		[Required]
		[DataType(DataType.MultilineText)]
		public string Message { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
