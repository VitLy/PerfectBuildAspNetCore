using System;
using System.Collections.Generic;

namespace PerfectBuild.Models.ViewModels
{
    public class UserRolesModel
    {
        public String UserId { get; set; }
        public String UserName { get; set; }
        public SortedDictionary<string,bool> Roles { get; set; }
    }
}
