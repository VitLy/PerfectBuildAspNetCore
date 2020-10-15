using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PerfectBuild.Models;

namespace PerfectBuild.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        //public ApplicationContext()
        //{

        //}
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Exercise>().HasOne(x => x.Unit).WithMany(x => x.Exercises).OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<TrainingProgramHead>().HasOne(x => x.Category).WithMany(x => x.TrainingProgramHeads).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<TrainingProgramSpec>().HasOne(x => x.TrainingProgramHead).WithMany(x => x.TrainingProgramSpecs).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<TrainingProgramSpec>().HasOne(x => x.Exercise).WithMany(x => x.TrainingProgramSpecs).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TrainingPlanSpec>().HasOne(x => x.TrainingPlanHead).WithMany(x => x.TrainingPlanSpec).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<TrainingPlanSpec>().HasOne(x => x.Exercise).WithMany(x => x.TrainingPlanSpecs).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TrainingSpec>().HasOne(x => x.TrainingHead).WithMany(x => x.TrainingSpec).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<TrainingSpec>().HasOne(x => x.Exercise).WithMany(x => x.TrainingSpecs).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
