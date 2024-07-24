using Deneme6.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Deneme6;
using Microsoft.EntityFrameworkCore;
using NoteApp.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//builder.Services.AddDbContext<BaseDbContext>(opt =>
//{
//    opt.UseNpgsql(configuration.GetConnectionString("Sql"));
//});


//builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IUserAction, UserAction>();
builder.Services.AddSingleton<INoteAction, NoteAction>(); //Genericlerin eklemesini yap.



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//Repoları açarak
// base repo oluşturulacak.