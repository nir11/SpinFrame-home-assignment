using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using server;
using server.Interfaces;
using server.Repository;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddEndpointsApiExplorer();
// Configure SwaggerGen to generate API document
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("api", new OpenApiInfo { Title = "API", Version = "v1" });
});

// Configure SwaggerGen to generate External API document
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("external", new OpenApiInfo { Title = "External API", Version = "v1" });
});
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // API document
        options.SwaggerEndpoint("/swagger/api/swagger.json", "API");

        // External API document
        options.SwaggerEndpoint("/swagger/external/swagger.json", "External API");
    });
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
