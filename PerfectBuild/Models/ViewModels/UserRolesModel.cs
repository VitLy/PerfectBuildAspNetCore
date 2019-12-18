using Microsoft.AspNetCore.Identity;
using PerfectBuild.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectBuild.Models.ViewModels
{
    public class UserRolesModel
    {
        public String UserId { get; set; }
        public String UserName { get; set; }
        public SortedDictionary<string,bool> Roles { get; set; }

    }
}
