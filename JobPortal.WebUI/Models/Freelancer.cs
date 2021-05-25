using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Models
{
    public class Freelancer

    {
        public class Skill
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string ExpertLevel { get; set; }
        }
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
