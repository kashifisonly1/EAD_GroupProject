using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalBlazor.Shared
{
	public class Freelancer
	{
		public int Id { get; set; }
		public ApplicationUser User { get; set; }
		public string Detail { get; set; }
		public List<Skill> Skills { get; set; }
	}
}
