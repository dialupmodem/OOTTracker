using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using OOTTracker.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRouting();
builder.Services.AddMvc();

var app = builder.Build();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseStaticFiles( new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "assets")),
    RequestPath = "/assets",
    ServeUnknownFileTypes = true
});

app.Run();
