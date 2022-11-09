using Gradebook.Foundation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Foundation.Domain;

public class FoundationModelsMappings
{
    public static void ConfigureModels(ModelBuilder builder)
    {
        builder.Entity<Student>(e =>
        {
            e.HasOne(e => e.CurrentClass)
            .WithMany(e => e.ActiveStudents)
            .HasForeignKey(e => e.CurrentClassGuid);
        });

        builder.Entity<Class>(e =>
        {
            e.HasMany(e => e.ActiveStudents)
            .WithOne(e => e.CurrentClass)
            .HasForeignKey(e => e.CurrentClassGuid);
        });
    }
}
