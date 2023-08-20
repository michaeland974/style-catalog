using style_catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace style_catalog.Data;

public class AccountContext : DbContext{
    public AccountContext (DbContextOptions<AccountContext> options)
        : base(options){}

    public DbSet<Account> Account { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder){
        base.OnModelCreating(builder);
    }
}

    

