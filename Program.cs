using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchoolContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SchoolContext>();
    // Note: if you're having trouble with EF, database schema, etc.,
    // uncomment the line below to re-create the database upon each run.
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context); //-->C:\Users\tony_huang\ContosoUniversity1.mdf
}




app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;
//var initialiser = services.GetRequiredService<DbInitializer>();

//var initialiser = app.Services.GetRequiredService<DbInitializer>();
//initialiser.Initialize();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


