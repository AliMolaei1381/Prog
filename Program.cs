using Microsoft.EntityFrameworkCore;
using Prog.Models.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// add Db
builder.Services.AddDbContext<Context_Db>
(
    options =>
    options.UseSqlServer
    (@"Data Source=DESKTOP-2RAFFTQ;initial Catalog=qq;integrated Security=SSPI;MultipleActiveResultSets=true;Trusted_Connection=True;TrustServerCertificate=True;")
);
builder.Services.AddMvc ().AddRazorRuntimeCompilation ();
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

app.UseRouting();

app.UseAuthorization();


app.MapAreaControllerRoute
(
    name: "Areas",
    areaName:"Admin",
    pattern: "Admin/{controller=Home}/{action=index}/{id?}"
);
app.MapControllerRoute
(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
