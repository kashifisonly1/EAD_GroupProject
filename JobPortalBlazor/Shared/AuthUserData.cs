using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalBlazor.Shared
{
    public class AuthUserData
    {
        public AspNetUser user { get; set; }
        public IList<string> roles { get; set; }
    }
}
