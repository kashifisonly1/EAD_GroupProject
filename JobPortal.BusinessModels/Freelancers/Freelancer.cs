using JobPortal.BusinessModels.General;

using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.BusinessModels.Freelancers
{
	public class Freelancer
	{
		public int Id { get; set; }
		public ApplicationUser User { get; set; }
		public string Detail { get; set; }
		public List<Skill> Skills { get; set; }
	}
}
