using DemoProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Data
{
    public class DemoProjectContext : DbContext
    {
        public DemoProjectContext()
        {
        }

        public DemoProjectContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>()
                .ToTable("Schools");

            modelBuilder.Entity<School>()
                .HasData(
                new School
                {
                    SchoolId = 1,
                    Name = "Today's Solutions",
                    SchoolCode = "123TSN"
                },
                new School
                {
                    SchoolId = 2,
                    Name = "Alphaone School",
                    SchoolCode = "456APS"
                },
                new School
                {
                    SchoolId = 3,
                    Name = "Alphaone Technologies",
                    SchoolCode = "456APT"
                },
                new School
                {
                    SchoolId = 4,
                    Name = "Amaka and Mary School",
                    SchoolCode = "456AMS"
                },
                new School
                {
                    SchoolId = 5,
                    Name = "JT Mentoring School",
                    SchoolCode = "456JTS"
                },
                new School
                {
                    SchoolId = 6,
                    Name = "MaryAmaka International School",
                    SchoolCode = "607MAI"
                }
                );
        }

    }


}
