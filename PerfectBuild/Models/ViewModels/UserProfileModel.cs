using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Models.ViewModels
{
    public class UserProfileModel
    {
        public Profile Profile { get; set; }
        [EmailAddress]
        public string EMail { get; set; }
    }
}
