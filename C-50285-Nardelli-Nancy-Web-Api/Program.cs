using C_50285_Nardelli_Nancy_Web_Api;
using C_50285_Nardelli_Nancy_Web_Api.DataAccess;
using C_50285_Nardelli_Nancy_Web_Api.Services.Interfaces;
using C_50285_Nardelli_Nancy_Web_Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("name=DefaultConection");
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWorkService>();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

 
builder.Services.AddCors( options =>
{
    options.AddDefaultPolicy( policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
