using Grpc.Core;
using Sample.GRPC.Server.API.Persistence;
using SampleComunicationServiceProto;

namespace Sample.GRPC.Server.API.SampleCrudService;

/// <summary>
/// Sample CRUD Service with GRPC
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
public class GrpcComunicationSampleService(ILogger<GrpcComunicationSampleService> logger, DummyContext dbContext)
    : SampleComunicationServiceApi.SampleComunicationServiceApiBase
{
    public override async Task<operationCompleteModel> UnaryCall(entityCreationRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override async Task StreamingFromServer(entitiesRequest request,
        IServerStreamWriter<entityModel> responseStream, ServerCallContext context)
    {
        while (!context.CancellationToken.IsCancellationRequested)
        {
            await responseStream.WriteAsync(new entityModel());
            await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
        }
    }
    public override async Task<responseEntitiesModel> StreamingFromClient(
        IAsyncStreamReader<entityCreationRequest> requestStream, ServerCallContext context)
    {
        await foreach (var message in requestStream.ReadAllAsync())
        {
            // ...
        }
        return new responseEntitiesModel();
    }


    public override async Task StreamingBothWays(IAsyncStreamReader<entitiesRequest> requestStream,
        IServerStreamWriter<entityModel> responseStream, ServerCallContext context)
    {
        await foreach (var message in requestStream.ReadAllAsync())
        {
            await responseStream.WriteAsync(new entityModel());
        }
    }


}
