using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLife.Model
{
    public class ApplicationUser : IdentityUser<string>
    {
        public long Experience { get; set; }

        public string GithubUsername { get; set; }

        public decimal Money { get; set; }
    }
}
