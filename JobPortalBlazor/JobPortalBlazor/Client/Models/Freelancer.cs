using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Models
{
    public class Freelancer : User
    {
        public List<Skill> UserSkill = new List<Skill>();

        [Required]
        [DataType(DataType.MultilineText)]
        public String Detail { get; set; }
        public void AddSkills(Skill s)
        {
            UserSkill.Add(s);
        }
    }
}
