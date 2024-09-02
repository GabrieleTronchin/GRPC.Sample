using Bogus;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Sample.GRPC.Server.API.Models;
using Sample.GRPC.Server.API.Persistence;
using SampleComunicationServiceProto;
using System.Text.Json;
using System.Threading;

namespace Sample.GRPC.Server.API.SampleComunicationService;

/// <summary>
/// Sample CRUD Service with GRPC
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
public class GrpcComunicationSampleService(
    ILogger<GrpcComunicationSampleService> logger,
    DummyContext dbContext
) : SampleComunicationServiceApi.SampleComunicationServiceApiBase
{
    public override async Task<operationCompleteModel> UnaryCall(
        entityCreationRequest request,
        ServerCallContext context
    )
    {
        throw new NotImplementedException();
    }

    public override async Task StreamingFromServer(
        entitiesRequest request,
        IServerStreamWriter<entityModel> responseStream,
        ServerCallContext context
    )
    {
        while (!context.CancellationToken.IsCancellationRequested)
        {
            var datas = GetFakeDbData();
            foreach (var data in datas)
            {
                await responseStream.WriteAsync(data.Adapt<entityModel>());
            }
        }
    }

    public List<DummyEntity> GetFakeDbData()
    {
        //simulate delay of db read
        Task.Delay(1000).Wait();

        return new Faker<DummyEntity>()
             .RuleFor(nameof(DummyEntity.Id), f => f.Random.Guid())
             .RuleFor(nameof(DummyEntity.Description), f => f.Random.String())
             .RuleFor(nameof(DummyEntity.Name), f => f.Random.String())
             .RuleFor(nameof(DummyEntity.ReferenceDate), f => f.Date.Recent())
             .GenerateBetween(1, 3);
    }


    public override async Task<responseEntitiesModel> StreamingFromClient(
        IAsyncStreamReader<entityCreationRequest> requestStream,
        ServerCallContext context
    )
    {
        await foreach (var message in requestStream.ReadAllAsync())
        {
            // ...
        }
        return new responseEntitiesModel();
    }

    public override async Task StreamingBothWays(
        IAsyncStreamReader<entitiesRequest> requestStream,
        IServerStreamWriter<entityModel> responseStream,
        ServerCallContext context
    )
    {
        await foreach (var message in requestStream.ReadAllAsync())
        {
            await responseStream.WriteAsync(new entityModel());
        }
    }
}
