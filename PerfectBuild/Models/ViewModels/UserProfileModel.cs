﻿using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class UserProfileModel
    {
        public Profile Profile { get; set; }
        [EmailAddress]
        public string EMail { get; set; }
    }
}
