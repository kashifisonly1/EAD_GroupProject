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

        public Freelancer() { }

        public Freelancer(JobPortalBlazor.Shared.Freelancer f) : base(f.User)
        {
            foreach (JobPortalBlazor.Shared.Skill skill in f.Skills)
                UserSkill.Add(new Skill(skill));
            this.Detail = f.Detail;
        }

        public static implicit operator JobPortalBlazor.Shared.Freelancer(Freelancer f)
        {
            JobPortalBlazor.Shared.Freelancer t = new JobPortalBlazor.Shared.Freelancer();
            foreach (Skill skill in f.UserSkill)
                t.Skills.Add(skill);
            t.Detail = f.Detail;
            t.UserId = f.UserID;
            return t;
        }
    }
}
