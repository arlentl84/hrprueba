using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using HRApi.DataAccess.Maps;
using HRApi.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;


namespace HRApi.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }       

        public ApplicationContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(new Role(1, "Worker"));
            builder.Entity<Role>().HasData(new Role(2, "Specialist"));
            builder.Entity<Role>().HasData(new Role(3, "Manager"));           


            base.OnModelCreating(builder);
            
            new SalaryIncreaseMap(builder.Entity<SalaryIncrease>());
            new SalaryRevisionMap(builder.Entity<SalaryRevision>());
            
            builder.Entity<Worker>().ToTable("Worker");
            builder.Entity<Role>().ToTable("Role");

           

            builder.Entity<WorkerSP>(entity => entity.HasNoKey().ToTable("WorkerSP", t => t.ExcludeFromMigrations()));
            builder.Entity<SalaryIncreaseSP>(entity => entity.HasNoKey().ToTable("SalaryIncreaseSP", t => t.ExcludeFromMigrations()));
                       

            /*builder.Entity<Worker>().HasMany(e => e.Roles).WithMany(e => e.Workers).UsingEntity(
            "RoleWorker",
            l => l.HasOne(typeof(Worker)).WithMany().HasForeignKey("WorkerId").HasPrincipalKey(nameof(Worker.Id)),
            r => r.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId").HasPrincipalKey(nameof(Role.Id)),
            j => j.HasKey("WorkerId", "RoleId"));*/

        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var conn = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(conn);
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server =.\\SQLExpress; Database = hr; Trusted_Connection = True; MultipleActiveResultSets = true; Encrypt = False");
        }

        public class ConciergeDBContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
        {
            public ApplicationContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

                var builder = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();

                var conn = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer("Server =.\\SQLExpress; Database = hr; Trusted_Connection = True; MultipleActiveResultSets = true; Encrypt = False");

                //optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=UserManager; User Id=prueba; Password=prueba123");

                return new ApplicationContext(optionsBuilder.Options);
            }
        }



        DbSet<Worker> Workers { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<SalaryIncrease> SalaryIncreases { get; set; }
        DbSet<SalaryRevision> SalaryRevisions { get; set; }
        public virtual DbSet<WorkerSP>  WorkerRoleSP { get; set; }

        public virtual DbSet<SalaryIncreaseSP> SalaryIncreasesSP { get; set; }

    }
}
