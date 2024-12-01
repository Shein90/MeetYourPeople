using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

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

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

static void ConfigureServices(IServiceCollection servicesCollection, IConfiguration configuration)
{
    servicesCollection.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

    servicesCollection.AddDbContext<MypDbContext>(options =>
    {
        var dbSettings = configuration.GetSection(DataBaseAccessSettings.SectionName).Get<DataBaseAccessSettings>();
        options.UseMySql(dbSettings?.ConnectionString, ServerVersion.AutoDetect(dbSettings?.ConnectionString));
    });
        
    servicesCollection.AddScoped<IUserManager, UserManager>();
    servicesCollection.AddScoped<IEventManager, EventManager>();
}
