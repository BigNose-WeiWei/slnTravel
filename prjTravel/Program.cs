using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

var dbstr = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={builder.Environment.ContentRootPath}App_Data\\dbProject.mdf;Integrated Security=True;Trusted_Connection=True;";

/*DB�s����m��אּ��dbstr����*/
builder.Services.AddDbContext<prjTravel.Models.TravelDbContext>
    (options => options.UseSqlServer(dbstr));

/*Cookie���Ҥ覡*/
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {   
        options.Cookie.HttpOnly = true;                                     //�g��Http��w�s��
        options.LoginPath = new PathString("/Home/Login");                  //���n�J�ɨϥ�Home��Login��k
        options.AccessDeniedPath = new PathString("/Home/NoAuthorization"); //�v�������ɩI�sHome��NoAuthorization��k
    });

var app = builder.Build();

app.UseStaticFiles();       //�ҥ��R�A�귽

app.UseCookiePolicy();      //�ҥ�Cookie�F��

app.UseAuthentication();    //�ҥΨ�������

app.UseAuthorization();     //�ҥΨ������v

/*�w�]������|*/
app.MapControllerRoute(name: "default",
    pattern: "{Controller=Home}/{Action=Index}/{id?}");

app.Run();
