using System.Text.Json.Serialization;
using TzRoutim.Model;
using TzRoutim.Services;
using TzRoutim.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc();

builder.Services.AddMvc().AddJsonOptions(o => {
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    o.JsonSerializerOptions.MaxDepth = 0;
});

// Получение строки подключения 
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Настройка подключения
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseMySql(connection, ServerVersion.AutoDetect(connection));
});

// Наттройка политики CORS
builder.Services.AddCors(opions =>
{
    opions.AddPolicy(name: "CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddTransient<ISearchServices, SearchServices>();
builder.Services.AddTransient<AddDataDb>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Index}/{action=Index}");
});

// Использование CORS
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
