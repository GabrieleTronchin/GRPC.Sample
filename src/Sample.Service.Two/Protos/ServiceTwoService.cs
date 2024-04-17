using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Sample.Service.Two.Models;
using Sample.Service.Two.Persistence;
using ServiceTwoProto;

namespace Sample.Service.Two.Protos;

/// <summary>
/// Sample CRUD Service with GRPC
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
public class ServiceTwoService(ILogger<ServiceTwoService> logger, DummyContext dbContext) : ServiceTwoApi.ServiceTwoApiBase
{


    /// <summary>
    ///
    /// </summary>
    /// <param name="request">Empty</param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<responseModel> GetEntities(Empty request, ServerCallContext context)
    {
        try
        {
            var lst = await dbContext.SampleEntities.ToListAsync();

            return new responseModel() { Success = true };
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
    public override async Task<singleResponseModel> CreateDummyEntity(DummyCreationRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogDebug("New Request received on {grpcServiceName}", nameof(ServiceTwoService));


            await dbContext.SampleEntities.AddAsync(new DummyEntity()
            {
                Id = new Guid(),
                Description = request.DummyEntity.Description,
                Name = request.DummyEntity.Name,
                ReferenceDate = request.DummyEntity.ReferenceDate.ToDateTime(),
            });

            await dbContext.SaveChangesAsync();

            logger.LogDebug("Request completed {grpcServiceName}", nameof(ServiceTwoService));

            return new singleResponseModel() { Success = true, Id = "Test" };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(ServiceTwoService));

            var errorRetModel = new singleResponseModel() { Success = false };
            errorRetModel.Exceptions.Add(new apiException() { Message = ex.Message, StatusCode = 500 });
            return errorRetModel;
        }
    }


}
