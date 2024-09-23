using Grpc.Core;
using Mapster;
using Sample.GRPC.Client.API.Models;
using SampleComunicationServiceProto;

namespace Sample.GRPC.Client.API.SampleCrudService;

public class SampleComunicationService : ISampleComunicationService
{
    private readonly SampleComunicationServiceApi.SampleComunicationServiceApiClient _client;
    private readonly Metadata _metadata;

    public SampleComunicationService(
        SampleComunicationServiceApi.SampleComunicationServiceApiClient client
    )
    {
        _client = client;
        _metadata = new Metadata
        {
            {
                "X-Apikey",
                "68e5fbda-9ec9-4858-97b2-4a8349764c63"
            } //just for test purpose
            ,
        };
    }

    public async Task<Guid> UnaryCall(SampleEntityPost request)
    {
        var response = await _client.UnaryCallAsync(
            request.Adapt<entityCreationRequest>(),
            _metadata
        );

        if (response.Success)
        {
            return Guid.Parse(response.Id);
        }

        throw new InvalidOperationException(
            string.Join(',', response.Exceptions.Select(x => x.Message))
        );
    }

    public async Task StreamingFromServer(
        IEnumerable<SampleEntityPost> request,
        CancellationToken cancellationToken
    )
    {
        var requests = request.Adapt<entitiesRequest>();
        while (!cancellationToken.IsCancellationRequested)
        {
            var responseStream = _client.StreamingFromServer(requests, _metadata);

            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        }
    }

    public async Task<responseEntitiesModel> StreamingFromClient(
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

    public async Task StreamingBothWays(
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
