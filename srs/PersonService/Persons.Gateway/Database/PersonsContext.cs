using Microsoft.EntityFrameworkCore;

namespace Persons.Gateway.Database;

#nullable disable
public class PersonContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
}
#nullable restore