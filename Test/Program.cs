using Common.Authentication;
using Common.Configuration;
using Common.Services.Abstract;
using Common.Services;
using DataAccess.Data;
using DataAccess.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.MapFallbackToFile("/index.html");

app.Run();

static void ConfigureServices(IServiceCollection servicesCollection, IConfiguration configuration)
{

    servicesCollection.AddOptionWithValidation<JwtAuthenticationSettings>(
        configuration.GetSection(JwtAuthenticationSettings.SectionName));

    servicesCollection.AddOptionWithValidation<DataBaseAccessSettings>(
        configuration.GetSection(DataBaseAccessSettings.SectionName));

    servicesCollection.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });

    servicesCollection.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

    servicesCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = servicesCollection
                .BuildServiceProvider()
                .GetRequiredService<IOptions<JwtAuthenticationSettings>>()
                .Value
                .GetDefaultTokenValidationParameters();
        });

    servicesCollection.AddDbContext<MypDbContext>(options =>
        options.UseMySql(
            "server=localhost;port=3306;database=myp_db;user=root;password=!CSer1990",
            new MySqlServerVersion(new Version(8, 0, 23))));
        //    mysqlOptions =>
        //    {
        //        mysqlOptions.EnableRetryOnFailure(
        //            maxRetryCount: 5,  // Максимальное количество попыток
        //            maxRetryDelay: TimeSpan.FromSeconds(10),  // Максимальное время ожидания между попытками
        //            errorNumbersToAdd: null  // Дополнительные коды ошибок, которые следует учитывать
        //        );
        //    })
        //.EnableSensitiveDataLogging()  // Включение логирования чувствительных данных (для отладки)
        //.EnableDetailedErrors()       // Включение более подробных ошибок
    //);

    servicesCollection.AddScoped<IUserManager, UserManager>();
    servicesCollection.AddScoped<IEventManager, EventManager>();
    servicesCollection.AddScoped<IPasswordService, PasswordService>();
}