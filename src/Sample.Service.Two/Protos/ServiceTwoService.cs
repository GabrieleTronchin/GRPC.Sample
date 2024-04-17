using Grpc.Core;
using ServiceTwoProto;

namespace Sample.Service.Two.Protos;


public class ServiceTwoService : ServiceTwoApi.ServiceTwoApiBase
{
    private readonly ILogger<ServiceTwoService> _logger;


    public ServiceTwoService(ILogger<ServiceTwoService> logger)
    {
        _logger = logger;

    }

    public override async Task<responseModel> CreateDummyEntity(DummyCreationRequest request, ServerCallContext context)
    {
        try
        {
            _logger.LogDebug("New Request received on {grpcServiceName}", nameof(ServiceTwoService));

            _logger.LogDebug("Request completed {grpcServiceName}", nameof(ServiceTwoService));

            return new responseModel() { Success = true, Id = "Test" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(ServiceTwoService));

            var errorRetModel = new responseModel() { Success = false };
            errorRetModel.Exceptions.Add(new apiException() { Message = ex.Message, StatusCode = 500 });
            return errorRetModel;
        }
    }
}
