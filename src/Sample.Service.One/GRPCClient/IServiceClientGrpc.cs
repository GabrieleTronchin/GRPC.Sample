
using SampleServiceProto;

namespace Sample.GRPC.Client.API.GRPCClient;

public interface IServiceClientGrpc
{
    Task<operationCompleteModel> CreateShowTime(CreationRequest request);
}