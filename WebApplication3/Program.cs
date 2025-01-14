using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConnection"));
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapGet("/", (ApplicationDbContext context) =>
//{
//    var emp = new Employee()
//    {
//        Name = "Test",
//        Email = "test@gmail.com"
//    };
//    context.Employee.Add(emp);
//    context.SaveChanges();

//    var emp2 = context.Employee.ToList();

//    return emp2;

//});

app.Run();
