using style_catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace style_catalog.Data;

public class UserContext : DbContext{
    public UserContext (DbContextOptions<UserContext> options)
        : base(options){}

    public DbSet<User> User { get; set; } = default!;
}

    

