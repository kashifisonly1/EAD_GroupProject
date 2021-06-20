using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortalBlazor.Shared
{
	public partial class Freelancer
	{
		public Freelancer()
		{
			Gigs = new HashSet<Gig>();
			Skills = new HashSet<Skill>();
		}

		public int Id { get; set; }
		public string UserId { get; set; }
		public string Detail { get; set; }

		public virtual AspNetUser User { get; set; }
		public virtual ICollection<Gig> Gigs { get; set; }
		public virtual ICollection<Skill> Skills { get; set; }
	}
}
