using DapperSystemAPI.Mapping;
using People.Application;
using People.Application.Database;

var builder = WebApplication.CreateBuilder(args);
//* load the connection string from configuration file
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Creating own extension
builder.Services.AddApplication(); //=> with this we have registered the interface
builder.Services.AddDatabase(config["ConnectionStrings:DataContext"]!);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//middleware is sequetial
app.UseMiddleware<ValidationMappingMiddleware>();

app.MapControllers();

//* for excutting the DbInitializer will be here
var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
