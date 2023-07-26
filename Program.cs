public class Program
{
    public static void Main(string[] args){
        string root = Directory.GetCurrentDirectory();
        string dotenv = Path.Combine(root, "enviornment.env");
        DotEnv.Load(dotenv);

        Build(args);
    }

    public static void Build(string[] args){
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
      
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
    }
}
