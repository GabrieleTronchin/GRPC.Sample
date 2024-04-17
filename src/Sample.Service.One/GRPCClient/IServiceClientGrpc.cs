using Sample.GRPC.Client.API.Models;

namespace Sample.GRPC.Client.API.GRPCClient
{
    public interface IServiceClientGrpc
    {
        Task<Guid> Create(SampleEntityPost request);
        Task Delete(string id);
        Task<IEnumerable<SampleEntityGet>> Get();
        Task<SampleEntityGet> GetSingle(string id);
        Task<Guid> Update(SampleEntityPut request);
    }
}