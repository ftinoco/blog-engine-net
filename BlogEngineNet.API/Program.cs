using BlogEngineNet.API.Utils;
using BlogEngineNet.API.Utils.Extensions;
using BlogEngineNet.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigSwaggerAuthentication();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddPersistenceDependencies(builder.Configuration);
builder.Services.ConfigAuthentication(builder);
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
 
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
