using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Models
{
    public class Contact
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public Contact() { }

        public Contact(JobPortalBlazor.Shared.SupportMessage c)
        {
            this.ID = c.Id;
            this.Name = c.User.FullName;
            this.Subject = c.Subject;
            this.Email = c.User.Email;
            this.Message = c.Message;
        }

        public static implicit operator JobPortalBlazor.Shared.SupportMessage(Contact c)
        {
            JobPortalBlazor.Shared.SupportMessage s = new JobPortalBlazor.Shared.SupportMessage();
            s.Id = c.ID;
            s.Subject = c.Subject;
            s.Message = c.Message;
            s.MessageDate = DateTime.UtcNow;
            //userID missing
            return s;
        }
    }
}
