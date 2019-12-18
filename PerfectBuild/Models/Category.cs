using System;
using System.ComponentModel.DataAnnotations;

namespace PerfectBuild.Models
{
    public partial class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(40), Required]
        public String Name { get; set; }
    }
}




