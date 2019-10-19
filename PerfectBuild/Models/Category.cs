using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectBuild.Model
{
    public partial class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(40), Required]
        public String Name { get; set; }
    }
}




