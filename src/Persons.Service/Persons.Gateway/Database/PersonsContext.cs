using Microsoft.EntityFrameworkCore;

namespace Persons.Gateway.Database;

#nullable disable
public class PersonsContext : DbContext
{
    protected PersonsContext()
    {
    }

    public PersonsContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PersonEntity> Persons { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonEntity>(x =>
        {
            x.Property(p => p.Name).IsRequired();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}
#nullable restore