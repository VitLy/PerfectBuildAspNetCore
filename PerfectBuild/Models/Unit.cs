using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(7, ErrorMessage="ShortNameMaxLength"),Required(ErrorMessage="ShortNameRequired", AllowEmptyStrings =false)]       
        public string ShortName { get; set; }
        [MaxLength(10,ErrorMessage = "NameMaxLength")]
        public string Name { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
