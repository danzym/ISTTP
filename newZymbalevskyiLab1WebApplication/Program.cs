using newZymbalevskyiLab1WebApplication;
using Microsoft.EntityFrameworkCore;
using newZymbalevskyiLab1WebApplication.Models;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DblibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<IdentityContext>(option => option.UseSqlServer(
	builder.Configuration.GetConnectionString("IdentityConnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

//builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleInitializer.InitializeAsync(userManager, rolesManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database." + DateTime.Now.ToString());
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();
//app.MapRazorPages();
//Unable to find the required services. Please add all the required services by calling
//'IServiceCollection.AddRazorPages' inside the call to 'ConfigureServices(...)'
//in the application startup code
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

/*https://localhost:7237/Account/Login?ReturnUrl=%2FCustomers
https://localhost:7237/Customers
*/