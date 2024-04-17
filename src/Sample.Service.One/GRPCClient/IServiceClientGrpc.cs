using Sample.GRPC.Client.API.Models;

namespace Sample.GRPC.Client.API.GRPCClient
{
    public interface IServiceClientGrpc
    {
        Task<Guid> Create(SampleEntityPost request);
        Task Delete(Guid id);
        Task<IEnumerable<SampleEntityGet>> Get();
        Task<SampleEntityGet> GetSingle(Guid id);
        Task<Guid> Update(SampleEntityPut request);
    }
}