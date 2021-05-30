using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Models
{
    public class Freelancer:User
    {
        public List<Skill> UserSkill = new List<Skill>();
        [Required]
        [DataType(DataType.MultilineText)]
        public String Detail { get; set; }
        //front require this 
        [Required]
        public String SkillList;
        public void AddSkills(Skill s)
        {
            UserSkill.Add(s);
        }
    }
}
