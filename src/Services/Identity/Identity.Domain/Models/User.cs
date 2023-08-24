using Microsoft.AspNetCore.Identity;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string? Photo { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; } = null!;
        public int Age { get; set; }
        public string? Biography { get; set; }
        public DateTime CreatedAt { get; set; } 
        public SexTypes SexTypeId { get; set; }

    }
}
