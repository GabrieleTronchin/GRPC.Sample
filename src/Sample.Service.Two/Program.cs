using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.GRPC.Server.API.Persistence;
using Sample.GRPC.Server.API.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();

builder.Services.AddDbContext<DummyContext>(options =>
{
    options.UseInMemoryDatabase("Dummy")
        .EnableSensitiveDataLogging();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/Get", async (DummyContext dbContext) =>
{
    return await dbContext.SampleEntities.ToListAsync();
})
.WithName("Get")
.WithOpenApi();

app.MapGet("/GetSingle/{payload}", async (DummyContext dbContext, [FromRoute] Guid payload) =>
{
    return await dbContext.SampleEntities.SingleAsync(x => x.Id == payload);
})
.WithName("GetSingle")
.WithOpenApi();



app.MapGrpcService<GrpcSampleService>();

app.Run();
