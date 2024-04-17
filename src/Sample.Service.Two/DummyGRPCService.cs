using Grpc.Core;
//using ServiceTwoProto;

namespace Sample.Service.Two;


public class DummyGRPCService //: ServiceTwoApi.GrpcService
{
    private readonly ILogger<DummyGRPCService> _logger;


    public DummyGRPCService(ILogger<DummyGRPCService> logger)
    {
        _logger = logger;

    }

    //public override async Task<responseModel> CreateShowTime(ShowtimeCreationRequest request, ServerCallContext context)
    //{
    //    try
    //    {
    //        _logger.LogDebug("New Request received on {grpcServiceName}", nameof(ShowtimeService));

    //        _logger.LogDebug("Request completed {grpcServiceName}", nameof(ShowtimeService));

    //        return new responseModel() { Success = true, ShotimeId = response.Id.ToString() };
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(ShowtimeService));
    //        var errorRetModel = new responseModel() { Success = false };
    //        errorRetModel.Exceptions.Add(new moviesApiException() { Message = ex.Message, StatusCode = 500 });
    //        return errorRetModel;
    //    }
    //}
}
