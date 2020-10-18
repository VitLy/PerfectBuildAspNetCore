using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models.ViewModels
{
    public class ChangeExerciseViewModel
    {
        public int Id { get; set; }
       
        [Required(ErrorMessage ="NameRequired")]
        [MaxLength(45,ErrorMessage ="NameMaxLength")]
        public string Name { get; set; }

        [MaxLength(250,ErrorMessage ="DescriptionMaxLength")]
        public string Description { get; set; }
        public int UnitId { get; set; }

        public bool OwnWeight { get; set; }

        public List<Unit> Units { get; set; }
    }
}
