using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRouting();
builder.Services.AddMvc();

builder.Services.AddScoped<ISpoilerFileProcessorService, SpoilerFileProcessorService>();

var app = builder.Build();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();

app.Run();
