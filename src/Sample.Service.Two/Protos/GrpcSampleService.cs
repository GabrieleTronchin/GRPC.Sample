using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Sample.GRPC.Server.API.Models;
using Sample.GRPC.Server.API.Persistence;
using SampleServiceProto;

namespace Sample.GRPC.Server.API.Protos;

/// <summary>
/// Sample CRUD Service with GRPC
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
public class GrpcSampleService(ILogger<GrpcSampleService> logger, DummyContext dbContext) : SampleServiceApi.SampleServiceApiBase
{


    /// <summary>
    ///
    /// </summary>
    /// <param name="request">Empty</param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<responseEntitiesModel> Gets(Empty request, ServerCallContext context)
    {
        try
        {
            var lst = await dbContext.SampleEntities.ToListAsync();

            return new responseEntitiesModel() { Success = true};
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(GrpcSampleService));

            var errorRetModel = new responseEntitiesModel() { Success = false };
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
    public override async Task<responseEntityModel> GetSingle(entityRequest request, ServerCallContext context)
    {
        try
        {
            var lst = await dbContext.SampleEntities.ToListAsync();

            return new responseEntityModel() { Success = true };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(GrpcSampleService));

            var errorRetModel = new responseEntityModel() { Success = false };
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
    public override async Task<operationCompleteModel> Create(CreationRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogDebug("New Request received on {grpcServiceName}", nameof(GrpcSampleService));

            var entity = new DummyEntity()
            {
                Id = new Guid(),
                Description = request.Item.Description,
                Name = request.Item.Name,
                ReferenceDate = request.Item.ReferenceDate.ToDateTime(),
            };

            await dbContext.SampleEntities.AddAsync(entity);

            await dbContext.SaveChangesAsync();

            logger.LogDebug("Request completed {grpcServiceName}", nameof(GrpcSampleService));

            return new operationCompleteModel() { Success = true, Id = entity.Id.ToString() };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(GrpcSampleService));

            var errorRetModel = new operationCompleteModel() { Success = false };
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
    public override async Task<operationCompleteModel> Update(UpdateRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogDebug("New Request received on {grpcServiceName}", nameof(GrpcSampleService));

            //await dbContext.SampleEntities.FirstOrDefault(x => x.Id == request.Item.Id);

            var entity = new DummyEntity()
            {
                Id = new Guid(),
                Description = request.Item.Description,
                Name = request.Item.Name,
                ReferenceDate = request.Item.ReferenceDate.ToDateTime(),
            };

            //await dbContext.SampleEntities.Update(entity);

            await dbContext.SaveChangesAsync();

            logger.LogDebug("Request completed {grpcServiceName}", nameof(GrpcSampleService));

            return new operationCompleteModel() { Success = true, Id = entity.Id.ToString() };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(GrpcSampleService));

            var errorRetModel = new operationCompleteModel() { Success = false };
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
    public override async Task<responseModel> Delete(entityRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogDebug("New Request received on {grpcServiceName}", nameof(GrpcSampleService));


            await dbContext.SaveChangesAsync();

            logger.LogDebug("Request completed {grpcServiceName}", nameof(GrpcSampleService));

            return new responseModel() { Success = true };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {grpcServiceName}", nameof(GrpcSampleService));

            var errorRetModel = new responseModel() { Success = false };
            errorRetModel.Exceptions.Add(new apiException() { Message = ex.Message, StatusCode = 500 });
            return errorRetModel;
        }
    }


}
