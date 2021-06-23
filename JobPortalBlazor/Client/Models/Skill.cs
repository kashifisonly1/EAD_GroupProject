using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Models
{
    public class Skill
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public Skill() { }
        public Skill(JobPortalBlazor.Shared.Skill s) {
            ID = s.Id;
            Name = s.Name;
        }

        public static implicit operator JobPortalBlazor.Shared.Skill(Skill s)
        {
            JobPortalBlazor.Shared.Skill sk = new JobPortalBlazor.Shared.Skill();
            sk.Name = s.Name;
            sk.Id = s.ID;
            return sk;
        }
    }
}
