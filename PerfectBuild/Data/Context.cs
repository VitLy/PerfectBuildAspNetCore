namespace PerfectBuild.Data
{
    using PerfectBuild.Model;
    using Microsoft.EntityFrameworkCore;

    public class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        #region DbSets

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Profile> Profiles { get; set; }

        public virtual DbSet<Exercise> Exercises { get; set; }

        public virtual DbSet<Param> Params { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<TrainingHead> TrainingHeads { get; set; }

        public virtual DbSet<TrainingSpec> TrainingSpecs { get; set; }

        public virtual DbSet<TrainingProgramHead> TrainingProgramHeads { get; set; }

        public virtual DbSet<TrainingProgramSpec> TrainingProgramSpecs { get; set; }

        public virtual DbSet<TrainingScheduler> TrainingSchedulers { get; set; }

        public virtual DbSet<Set> Sets { get; set; }

        #endregion
    }
}