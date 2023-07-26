using style_catalog.Data;
using Microsoft.EntityFrameworkCore;

string root = Directory.GetCurrentDirectory();
string dotenv = Path.Combine(root, "enviornment.env");
DotEnv.Load(dotenv);

var builder = WebApplication.CreateBuilder(args);
var connection = Environment.GetEnvironmentVariable("POSTGRESQL__CONNECTIONSTRING");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString(connection) ?? 
        throw new InvalidOperationException("Connection string not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()){
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

