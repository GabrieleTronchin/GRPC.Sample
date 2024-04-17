using Grpc.Core;
using ServiceTwoProto;

namespace Sample.GRPC.Client.API.GRPCClient;

public class ServiceClientGrpc : IServiceClientGrpc
{
    private readonly ServiceTwoApi.ServiceTwoApiClient _client;
    private readonly Metadata _metadata;

    public ServiceClientGrpc(ServiceTwoApi.ServiceTwoApiClient client)
    {
        _client = client;
        _metadata = new Metadata
        {
            { "X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63" } //just for test purpose
        };
    }
    public async Task<singleResponseModel> CreateShowTime(DummyCreationRequest request)
    {
        return await _client.CreateDummyEntityAsync(request, _metadata);
    }

}