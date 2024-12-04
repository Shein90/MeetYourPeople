using Common.Authentication;
using DataAccess.DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();

app.UseStaticFiles();

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
        configuration.GetConnectionString("ConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 23))));

    servicesCollection.AddScoped<IUserManager, UserManager>();
    servicesCollection.AddScoped<IEventManager, EventManager>();
    servicesCollection.AddScoped<IPasswordService, PasswordService>();
}