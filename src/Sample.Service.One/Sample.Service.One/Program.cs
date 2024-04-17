using Sample.Service.One;
using Sample.Service.One.GRPCClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSampleGRPCClient(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/Create", async (IServiceTwoClientGrpc service) =>
{
    return await service.CreateShowTime(new ServiceTwoProto.DummyCreationRequest()
    {
        DummyEntity = new ServiceTwoProto.dummyEntityRequest()
        {
            Description = "test",
            Name = "testName",
            ReferenceDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
        }
    }); ;
})
.WithName("GetDataFromServiceTwo")
.WithOpenApi();

app.Run();