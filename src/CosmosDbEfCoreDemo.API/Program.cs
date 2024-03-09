using CosmosDbEfCoreDemo.API.Configuration;
using CosmosDbEfCoreDemo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<CosmosDbContext>(optionsBuilder =>
  optionsBuilder
    .UseCosmos(
      connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
      databaseName: "ApplicationDB",
      cosmosOptionsAction: options =>
      {
          options.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Direct);
          options.MaxRequestsPerTcpConnection(16);
          options.MaxTcpConnectionsPerEndpoint(32);
      }));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
