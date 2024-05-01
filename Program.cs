using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Portal.Data.Context;
using Portal.Data.Dal;
using Portal.MiddleWare;
using Portal.Services.Hash;
using Portal.Services.Kdf;
using Portal.Services.Upload;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHashService, Md5HashService>();
builder.Services.AddSingleton<IKdfService, PbKdf1Service>();
builder.Services.AddSingleton<IUploadService, UploadServiceV1>();
builder.Services.AddSingleton<DataAccessor>();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("MsSql")), ServiceLifetime.Singleton);

//налаштування сесії
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

app.UseSession(); // налаштування сесії
app.UseSessionAuth();//наш middleware для автентифікації через сесії

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
