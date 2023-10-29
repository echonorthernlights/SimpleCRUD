using SimpleCRUD.Web.DataAccess;
using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);
//// Add services
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie();

// Add services to the container.
builder.Services.AddControllersWithViews();
// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DB Context
builder.Services.AddDbContext<PeopleContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));



// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<PeopleContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
//Customize unauthenticated login route
//builder.Services.Configure<CookieAuthenticationOptions>(options =>
//{
//    options.LoginPath = "/Auth/Login"; // Change this to your desired login route
//});

// Enable Validation : Email validation  method
builder.Services.AddMvc().AddViewOptions(options =>
{
    options.HtmlHelperOptions.ClientValidationEnabled = true;
});

// Customize the login path
builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
{
    options.LoginPath = "/Auth/Login"; // Change this to your desired login route
    options.AccessDeniedPath = "/Auth/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


// Add authentication middleware
app.UseAuthentication();
// Use swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
