using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;
using Sample.Service.Two.Models;
using ServiceTwoProto;

namespace Sample.Service.Two.Protos;

/// <summary>
/// Sample CRUD Service with GRPC
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
public class ServiceTwoService(ILogger<ServiceTwoService> logger, IMemoryCache cache) : ServiceTwoApi.ServiceTwoApiBase
{


    /// <summary>
    ///
    /// </summary>
    /// <param name="request">Empty</param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<responseModel> GetEntities(Empty request, ServerCallContext context) {
        try
        {

            return new responseModel() { Success = true, Id = "Test" };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(ServiceTwoService));

            var errorRetModel = new responseModel() { Success = false };
            errorRetModel.Exceptions.Add(new apiException() { Message = ex.Message, StatusCode = 500 });
            return errorRetModel;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<responseModel> CreateDummyEntity(DummyCreationRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogDebug("New Request received on {grpcServiceName}", nameof(ServiceTwoService));


            cache.Set("", new DummyEntity());

            logger.LogDebug("Request completed {grpcServiceName}", nameof(ServiceTwoService));

            return new responseModel() { Success = true, Id = "Test" };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(ServiceTwoService));

            var errorRetModel = new responseModel() { Success = false };
            errorRetModel.Exceptions.Add(new apiException() { Message = ex.Message, StatusCode = 500 });
            return errorRetModel;
        }
    }


}
