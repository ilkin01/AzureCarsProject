using App.Business.Abstract;
using App.Business.Concrete;
using App.DataAccess.Abstract;
using App.DataAccess.Concrete.EfEntityFramework;
using App.DataAccess.Concrete.EFEntityFramework;
using App.Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICloudinaryImageService, CloudinaryImageService>();

builder.Services.AddControllers().AddJsonOptions(x =>
				x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
	Formatting = Newtonsoft.Json.Formatting.Indented,
	ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
};
//builder.Services.AddScoped<IAutoSalonService, AutoSalonService>();
//builder.Services.AddScoped<IAutoSalonDal, EfAutoSalonDal>();

//builder.Services.AddScoped<ICarService, CarService>();
//builder.Services.AddScoped<ICarDal, EFCarDal>();

//builder.Services.AddScoped<ICodeGeneratorService, GenerateCodeService>();

//builder.Services.AddScoped<IEngineService, EngineService>();
//builder.Services.AddScoped<IEngineDal, EFEngineDal>();

//builder.Services.AddScoped<IFavoriteService, FavoriteService>();
//builder.Services.AddScoped<IFavoriteDal, EFFavoriteDal>();

//builder.Services.AddScoped<IFinanceService, FinanceService>();
//builder.Services.AddScoped<IFinanceDal, EFFinanceDal>();

//builder.Services.AddScoped<IImageService, ImageService>();
//builder.Services.AddScoped<IImageDal, EFImageDal>();

//builder.Services.AddScoped<ILoginService, LoginService>();

//builder.Services.AddScoped<IMainService, MainService>();
//builder.Services.AddScoped<IMainDal, EFMainDal>();

//builder.Services.AddScoped<IPostService, PostService>();
//builder.Services.AddScoped<IPostDal, EFPostDal>();

//builder.Services.AddScoped<ISmsSenderService, SmsSenderService>();

//builder.Services.AddScoped<IStatusService, StatusService>();
//builder.Services.AddScoped<IStatusDal, EFStatusDal>();

//builder.Services.AddScoped<IUserHelperService, UserHelperService>();
//builder.Services.AddScoped<IStatusDal, EFStatusDal>();

//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserDal, EFUserDal>();

//builder.Services.AddScoped<INotificationService, NotificationService>();
//builder.Services.AddScoped<INotificationDal, EFNotificationDal>();

//builder.Services.AddScoped<IFilterService, FilterService>();
//builder.Services.AddScoped<IHelperService, HelperService>();

//builder.Services.AddScoped<IAesEncryptionServiceV2, AesEncryptionServiceV2>();

builder.Services.AddScoped<IAutoSalonService, AutoSalonService>();
builder.Services.AddScoped<IAutoSalonDal, EfAutoSalonDal>();

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarDal, EFCarDal>();

builder.Services.AddScoped<ICodeGeneratorService, GenerateCodeService>();

builder.Services.AddSingleton<IEngineService, EngineService>();
builder.Services.AddSingleton<IEngineDal, EFEngineDal>();

builder.Services.AddSingleton<IFavoriteService, FavoriteService>();
builder.Services.AddSingleton<IFavoriteDal, EFFavoriteDal>();

builder.Services.AddSingleton<IFinanceService, FinanceService>();
builder.Services.AddSingleton<IFinanceDal, EFFinanceDal>();

builder.Services.AddSingleton<IImageService, ImageService>();
builder.Services.AddSingleton<IImageDal, EFImageDal>();

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddSingleton<IMainService, MainService>();
builder.Services.AddSingleton<IMainDal, EFMainDal>();

builder.Services.AddSingleton<IPostService, PostService>();
builder.Services.AddSingleton<IPostDal, EFPostDal>();

builder.Services.AddScoped<ISmsSenderService, SmsSenderService>();

builder.Services.AddSingleton<IStatusService, StatusService>();
builder.Services.AddSingleton<IStatusDal, EFStatusDal>();

builder.Services.AddScoped<IUserHelperService, UserHelperService>();
builder.Services.AddScoped<IStatusDal, EFStatusDal>();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserDal, EFUserDal>();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationDal, EFNotificationDal>();

builder.Services.AddSingleton<IFilterService, FilterService>();
builder.Services.AddSingleton<IHelperService, HelperService>();

builder.Services.AddScoped<IAesEncryptionServiceV2, AesEncryptionServiceV2>();





builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
		ValidAudience = builder.Configuration["JwtSettings:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey
		(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = false,
		ValidateIssuerSigningKey = true
	};
});

builder.Services.AddAuthorization();


var conn = builder.Configuration.GetConnectionString("myconn");

builder.Services.AddDbContext<CarDBContext>(opt =>
{
    opt.UseSqlServer(conn);
});
builder.Services.AddMemoryCache();
var app = builder.Build();




//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Car}/{action=Cars}/{id?}");

app.Run();
