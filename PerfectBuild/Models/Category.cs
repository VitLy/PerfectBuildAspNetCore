using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models
{
    public partial class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(40,ErrorMessage ="MaxLength"), Required(ErrorMessage ="NameRequired")]
        public String Name { get; set; }

        public virtual ICollection<TrainingProgramHead> TrainingProgramHeads { get; set; }
    }
}




