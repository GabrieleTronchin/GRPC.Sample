using ServiceTwoProto;

namespace Sample.Service.One.GRPCClient;

public interface IServiceTwoClientGrpc
{
    Task<singleResponseModel> CreateShowTime(DummyCreationRequest request);
}