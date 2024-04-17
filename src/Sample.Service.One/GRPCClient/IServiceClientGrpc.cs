using ServiceTwoProto;

namespace Sample.GRPC.Client.API.GRPCClient;

public interface IServiceClientGrpc
{
    Task<singleResponseModel> CreateShowTime(DummyCreationRequest request);
}