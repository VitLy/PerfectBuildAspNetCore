using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(7),Required(AllowEmptyStrings =false)]       
        public string ShortName { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }                           
    }
}
