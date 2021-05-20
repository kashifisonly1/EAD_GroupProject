using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Models
{
    public class Freelancer:PageModel

    {
        public class Skill
        {
            public string Name { get; set; }
            public string ExpertLevel { get; set; }
        }

        public int ID { get; set; }
        List<Skill> UserSkill = new List<Skill>();
        [Required]
        [DataType(DataType.MultilineText)]
        public String Detail { get; set; }
        [Required]
        public String WorkingHours { get; set; }
        [Required]
        public String Occupation { get; set; }

        public void AddSkills(Skill s)
        {
            UserSkill.Add(s);
       
        }
    }
}
