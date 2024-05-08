using Mapster;
using Microsoft.AspNetCore.Mvc;
using Sample.GRPC.Client.API;
using Sample.GRPC.Client.API.GRPCClient;
using Sample.GRPC.Client.API.Mapster;
using Sample.GRPC.Client.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSampleGRPCClient(builder.Configuration);
builder.Services.AddMapster();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet(
        "/Get",
        async (IServiceClientGrpc service) =>
        {
            return await service.Get();
        }
    )
    .WithName("Get")
    .WithOpenApi();

app.MapGet(
        "/GetSingle/{payload}",
        async (IServiceClientGrpc service, [FromRoute] string payload) =>
        {
            return await service.GetSingle(payload);
        }
    )
    .WithName("GetSingle")
    .WithOpenApi();

app.MapPost(
        "/Create",
        async (IServiceClientGrpc service, [FromBody] SampleEntityPost payload) =>
        {
            return await service.Create(payload);
        }
    )
    .WithName("Create")
    .WithOpenApi();

app.MapPut(
        "/Update",
        async (IServiceClientGrpc service, [FromBody] SampleEntityPut payload) =>
        {
            return await service.Update(payload);
        }
    )
    .WithName("Update")
    .WithOpenApi();

app.MapDelete(
        "/Delete/{payload}",
        async (IServiceClientGrpc service, [FromRoute] string payload) =>
        {
            await service.Delete(payload);
        }
    )
    .WithName("Delete")
    .WithOpenApi();

MapsterSettings.Configure();

app.Run();
