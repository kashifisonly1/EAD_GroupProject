using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Models
{
    public class Chat
    {
        public int ID { get; set; }
        public String Message;
        public int SenderID;
        public int ReceiverID;
        public DateTime Date;
        public User sender;
        public User receiver;
    }
}
