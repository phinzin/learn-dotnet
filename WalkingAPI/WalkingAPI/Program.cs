using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WalkingAPI;
using WalkingAPI.Mappings;
using WalkingAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WalkDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("WalkConnectionString")));

// Map interface
builder.Services.AddScoped<IRegionRepository, SqlRegionRepository>();
builder.Services.AddScoped<IDifficultyRepository, SqlDifficultyRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; // This makes Swagger UI the default page
    });


}

// custom exception handler
app.UseMiddleware<ExceptionHandlerMiddleware>();


// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();