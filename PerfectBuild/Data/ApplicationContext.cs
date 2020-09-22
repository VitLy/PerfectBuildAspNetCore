using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PerfectBuild.Models;

namespace PerfectBuild.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext()
        {

        }
        public ApplicationContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Param> Params { get; set; }
        public virtual DbSet<TrainingPlanHead> TrainingPlanHeads { get; set; }
        public virtual DbSet<TrainingPlanSpec> TrainingPlanSpecs { get; set; }
        public virtual DbSet<TrainingProgramHead> TrainingProgramHeads { get; set; }
        public virtual DbSet<TrainingProgramSpec> TrainingProgramSpecs { get; set; }
        public virtual DbSet<TrainingHead> TrainingHeads { get; set; }
        public virtual DbSet<TrainingSpec> TrainingSpecs{ get; set; }
    }
}
