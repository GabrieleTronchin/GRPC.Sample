using Mapster;
using Sample.GRPC.Client.API;
using Sample.GRPC.Client.API.Endpoints;
using Sample.GRPC.Client.API.Mapster;

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

app.AddCRUDEndpoints();

MapsterSettings.Configure();

app.Run();
