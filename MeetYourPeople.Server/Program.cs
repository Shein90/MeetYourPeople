using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("MYAPP_CONNECTIONSTRING");

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.MapFallbackToFile("/index.html");

app.Run();

static void ConfigureServices(IServiceCollection servicesCollection, IConfiguration configuration)
{
    servicesCollection.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });

    servicesCollection.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

    servicesCollection.AddDbContext<MypDbContext>(options =>
    {
        var connectionString = Environment.GetEnvironmentVariable("MYAPP_CONNECTIONSTRING");
        //var dbSettings = configuration.GetSection(DataBaseAccessSettings.SectionName).Get<DataBaseAccessSettings>();
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });
        
    servicesCollection.AddScoped<IUserManager, UserManager>();
    servicesCollection.AddScoped<IEventManager, EventManager>();
}
