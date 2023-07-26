namespace style_catalog.Models;

using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext{
    protected readonly IConfiguration Configuration;
    public DataContext(IConfiguration configuration){
        Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options){
        var connection = Environment.GetEnvironmentVariable("POSTGRESQL__CONNECTIONSTRING");
        options.UseNpgsql(connection);
    }
    public DbSet<User> Users { get; set; }
}