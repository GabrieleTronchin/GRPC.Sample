using ServiceTwoProto;

namespace Sample.Service.One.GRPCClient;

public interface IServiceTwoClientGrpc
{
    Task<responseModel> CreateShowTime(DummyCreationRequest request);
}