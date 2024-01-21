using UTC2_DKHP_Server.Hubs;
using UTC2_DKHP_Server.IRepositories;
using UTC2_DKHP_Server.Models.DKHPMobile;
using UTC2_DKHP_Server.Models.Login;
using UTC2_DKHP_Server.Repositories;
using UTC2_DKHP_Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient();


var mobileUrls = builder.Configuration
		  .GetSection("MobileUrls")
		  .Get<MobileUrls>();

builder.Services.AddSingleton<MobileUrls>(mobileUrls!);

builder.Services.AddScoped<IMobileHocPhanRepository, MobileRepository>();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddSignalR();
//builder.Services.AddSignalR(e => {
//    e.MaximumReceiveMessageSize = 102400000;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<MobileHub>("/hubs/dkhp");

app.Run();
