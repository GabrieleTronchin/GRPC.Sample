using Grpc.Core;
using SampleServiceProto;

namespace Sample.GRPC.Client.API.GRPCClient;

public class ServiceClientGrpc : IServiceClientGrpc
{
    private readonly SampleServiceApi.SampleServiceApiBase _client;
    private readonly Metadata _metadata;

    public ServiceClientGrpc(SampleServiceApi.SampleServiceApiBase client)
    {
        _client = client;
        _metadata = new Metadata
        {
            { "X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63" } //just for test purpose
        };
    }
    public async Task<operationCompleteModel> CreateShowTime(CreationRequest request)
    {
        return await _client.Create(request, _metadata);
    }

}