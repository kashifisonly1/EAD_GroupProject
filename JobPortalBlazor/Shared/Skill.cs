using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortalBlazor.Shared
{
	public partial class Skill
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? FreelancerId { get; set; }

		public virtual Freelancer Freelancer { get; set; }
	}
}
