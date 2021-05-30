using JobPortal.BusinessModels.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.Freelancers
{
    public class Freelancer
    {
        public ApplicationUser UserID { get; set; }
        public String Detail { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
