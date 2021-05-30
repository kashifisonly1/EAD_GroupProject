using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.General
{
    class UserRole
    {
        public ApplicationUser UserID { get; set; }
        public ApplicationRole RoleID { get; set; }
    }
}
