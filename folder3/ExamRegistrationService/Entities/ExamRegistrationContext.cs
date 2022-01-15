using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamRegistrationService.Entities
{
    public class ExamRegistrationContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ExamRegistrationContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<ExamRegistration> ExamRegistrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ExamRegistrationDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ExamRegistration>()
                .HasData(new
                {
                    ExamRegistrationId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    StudentId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    StudentFirstName = "Petar",
                    StudentLastName = "Petrović",
                    StudentIndex = "IT1/2020",
                    StudentCurrentSemester = 1,
                    StudentEnrolledYear = 1,
                    SubjectId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    SubjectCode = "S12020",
                    SubjectName = "Subject 1",
                    SubjectTerm = 1,
                    SubjectSemester = 1,
                    SubjectExamTime = DateTime.Parse("2020-11-15T09:00:00")
                });

            builder.Entity<ExamRegistration>()
                .HasData(new
                {
                    ExamRegistrationId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    StudentId = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    StudentFirstName = "Marko",
                    StudentLastName = "Marković",
                    StudentIndex = "IT2/2019",
                    StudentCurrentSemester = 1,
                    StudentEnrolledYear = 2,
                    SubjectId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    SubjectCode = "S22020",
                    SubjectName = "Subject 2",
                    SubjectTerm = 1,
                    SubjectSemester = 2,
                    SubjectExamTime = DateTime.Parse("2020-11-15T09:00:00")
                });
        }
    }
}
