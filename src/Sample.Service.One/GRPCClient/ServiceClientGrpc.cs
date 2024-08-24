using Grpc.Core;
using Mapster;
using Sample.GRPC.Client.API.Models;
using SampleServiceProto;

namespace Sample.GRPC.Client.API.GRPCClient;

public class ServiceClientGrpc : IServiceClientGrpc
{
    private readonly SampleServiceApi.SampleServiceApiClient _client;
    private readonly Metadata _metadata;

    public ServiceClientGrpc(SampleServiceApi.SampleServiceApiClient client)
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

    public async Task<IEnumerable<SampleEntityGet>> Get()
    {
        var response = await _client.GetsAsync(new Empty(), _metadata);

        if (response.Success)
        {
            return response.Items.Adapt<IEnumerable<SampleEntityGet>>();
        }

        throw new InvalidOperationException(
            string.Join(',', response.Exceptions.Select(x => x.Message))
        );
    }

    public async Task<SampleEntityGet> GetSingle(string id)
    {
        var response = await _client.GetSingleAsync(new entityRequest() { Id = id }, _metadata);

        if (response.Success)
        {
            return response.Item.Adapt<SampleEntityGet>();
        }

        throw new InvalidOperationException(
            string.Join(',', response.Exceptions.Select(x => x.Message))
        );
    }

    public async Task<Guid> Create(SampleEntityPost request)
    {
        var response = await _client.CreateAsync(request.Adapt<CreationRequest>(), _metadata);

        if (response.Success)
        {
            return Guid.Parse(response.Id);
        }

        throw new InvalidOperationException(
            string.Join(',', response.Exceptions.Select(x => x.Message))
        );
    }

    public async Task<Guid> Update(SampleEntityPut request)
    {
        var response = await _client.UpdateAsync(request.Adapt<UpdateRequest>(), _metadata);

        if (response.Success)
        {
            return Guid.Parse(response.Id);
        }

        throw new InvalidOperationException(
            string.Join(',', response.Exceptions.Select(x => x.Message))
        );
    }

    public async Task Delete(string id)
    {
        var response = await _client.DeleteAsync(new entityRequest() { Id = id }, _metadata);

        if (!response.Success)
        {
            throw new InvalidOperationException(
                string.Join(',', response.Exceptions.Select(x => x.Message))
            );
        }
    }
}
