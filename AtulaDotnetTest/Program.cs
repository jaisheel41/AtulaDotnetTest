using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AtulaDotnetTest.Models;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))
    ));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductDTOValidator>();

// Build the application
var app = builder.Build();

// Seed initial users
await SeedTestUser(app.Services.CreateScope().ServiceProvider);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure authentication middleware is added
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

// Seeding a test user
async Task SeedTestUser(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Create a test user
    var testUser = new ApplicationUser
    {
        UserName = "admin@example.com",
        Email = "admin@example.com",
        FirstName = "Admin",
        LastName = "User"
    };

    // Check if the test user exists, and create it if not
    var user = await userManager.FindByEmailAsync(testUser.Email);
    if (user == null)
    {
        var result = await userManager.CreateAsync(testUser, "Admin@123");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(testUser, "Admin");
        }
    }
}
