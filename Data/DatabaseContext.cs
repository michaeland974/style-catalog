using style_catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace style_catalog.Data;

public class DatabaseContext : DbContext{
    public DatabaseContext (DbContextOptions<DatabaseContext> options)
        : base(options){}

    public DbSet<Account> Account { get; set; } = default!;
    public DbSet<Mixin> Mixin { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder){
        base.OnModelCreating(builder);
    }
}

    

