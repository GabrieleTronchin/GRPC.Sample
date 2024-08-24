using Mapster;
using Microsoft.EntityFrameworkCore;
using Sample.GRPC.Server.API.Endpoints;
using Sample.GRPC.Server.API.Mapster;
using Sample.GRPC.Server.API.Persistence;
using Sample.GRPC.Server.API.SampleComunicationService;
using Sample.GRPC.Server.API.SampleCrudService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapster();
builder.Services.AddGrpc();

builder.Services.AddDbContext<DummyContext>(options =>
{
    options.UseInMemoryDatabase("Dummy").EnableSensitiveDataLogging();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddCRUDEndpoints();
app.AddComunicationEndpoints();

app.MapGrpcService<GrpcCrudSampleService>();
app.MapGrpcService<GrpcComunicationSampleService>();

MapsterSettings.Configure();

app.Run();
