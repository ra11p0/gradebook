using Gradebook.Foundation.Domain;
using Gradebook.Foundation.Domain.Models;
using Gradebook.Foundation.Domain.Models.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gradebook.Foundation.Database;

public class FoundationDatabaseContext : DbContext
{

    public DbSet<Class>? Classes { get; set; }
    public DbSet<Grade>? Grades { get; set; }
    public DbSet<Group>? Groups { get; set; }
    public DbSet<Lesson>? Lessons { get; set; }
    public DbSet<Position>? Positions { get; set; }
    public DbSet<School>? Schools { get; set; }
    public DbSet<Student>? Students { get; set; }
    public DbSet<StudentsAbsence>? StudentsAbsences { get; set; }
    public DbSet<Subject>? Subjects { get; set; }
    public DbSet<Teacher>? Teachers { get; set; }
    public DbSet<TeachersAbsence>? TeachersAbsences { get; set; }
    public DbSet<SystemInvitation>? SystemInvitations { get; set; }
    public DbSet<Administrator>? Administrators { get; set; }
    public DbSet<EducationCycle>? EducationCycles { get; set; }
    public DbSet<EducationCycleStep>? EducationCycleSteps { get; set; }
    public DbSet<EducationCycleStepSubject>? EducationCycleStepSubjects { get; set; }
    public DbSet<EducationCycleInstance>? EducationCycleInstances { get; set; }
    public DbSet<EducationCycleStepInstance>? EducationCycleStepInstances { get; set; }
    public DbSet<EducationCycleStepSubjectInstance>? EducationCycleStepSubjectInstances { get; set; }
    public DbSet<Email>? MailHistory { get; set; }

    public FoundationDatabaseContext()
    {

    }
    public FoundationDatabaseContext(DbContextOptions<FoundationDatabaseContext> options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        FoundationModelsMappings.ConfigureModels(builder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var cfg = builder.Build();
            var cn = cfg.GetConnectionString("DefaultAppDatabase");
            optionsBuilder.UseMySql(
                cn,
                new MySqlServerVersion(new Version(8, 30, 0)),
                e => e.MigrationsHistoryTable("__FoundationMigrationsHistory")
            );
        }
    }
    public async Task Migrate()
    {
        await this.Database.MigrateAsync();
    }
}
