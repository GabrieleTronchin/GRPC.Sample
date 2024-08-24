using Grpc.Core;
using Mapster;
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
public class GrpcSampleService(ILogger<GrpcSampleService> logger, DummyContext dbContext)
    : SampleServiceApi.SampleServiceApiBase
{
    public override async Task<responseEntitiesModel> Gets(Empty request, ServerCallContext context)
    {
        try
        {
            var lst = await dbContext.SampleEntities.ToListAsync();

            var response = new responseEntitiesModel() { Success = true };
            response.Items.AddRange(lst.Adapt<IEnumerable<entityModel>>());

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {Gets}", nameof(Gets));

            var errorRetModel = new responseEntitiesModel() { Success = false };
            errorRetModel.Exceptions.Add(
                new apiException() { Message = ex.Message, StatusCode = 500 }
            );
            return errorRetModel;
        }
    }

    public override async Task<responseEntityModel> GetSingle(
        entityRequest request,
        ServerCallContext context
    )
    {
        try
        {
            var obj = await dbContext.SampleEntities.SingleAsync(x =>
                x.Id == Guid.Parse(request.Id)
            );

            var response = new responseEntityModel()
            {
                Success = true,
                Item = obj.Adapt<entityModel>(),
            };

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {GetSingle}", nameof(GetSingle));

            var errorRetModel = new responseEntityModel() { Success = false };
            errorRetModel.Exceptions.Add(
                new apiException() { Message = ex.Message, StatusCode = 500 }
            );
            return errorRetModel;
        }
    }

    public override async Task<operationCompleteModel> Create(
        CreationRequest request,
        ServerCallContext context
    )
    {
        try
        {
            logger.LogDebug("New Request received on {Create}", nameof(Create));

            var entity = new DummyEntity()
            {
                Id = new Guid(),
                Description = request.Item.Description,
                Name = request.Item.Name,
                ReferenceDate = request.Item.ReferenceDate.ToDateTime(),
                LastTimeModified = DateTime.UtcNow,
            };

            await dbContext.SampleEntities.AddAsync(entity);

            await dbContext.SaveChangesAsync();

            logger.LogDebug("Request completed {Create}", nameof(Create));

            return new operationCompleteModel() { Success = true, Id = entity.Id.ToString() };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {Create}", nameof(Create));

            var errorRetModel = new operationCompleteModel() { Success = false };
            errorRetModel.Exceptions.Add(
                new apiException() { Message = ex.Message, StatusCode = 500 }
            );
            return errorRetModel;
        }
    }

    public override async Task<operationCompleteModel> Update(
        UpdateRequest request,
        ServerCallContext context
    )
    {
        try
        {
            logger.LogDebug("New Request received on {Update}", nameof(Update));

            var obj = await dbContext.SampleEntities.SingleAsync(x =>
                x.Id == Guid.Parse(request.Item.Id)
            );

            obj.Name = request.Item.Name;
            obj.Description = request.Item.Description;
            obj.ReferenceDate = request.Item.ReferenceDate.ToDateTime();
            obj.LastTimeModified = DateTime.UtcNow;

            dbContext.SampleEntities.Update(obj);

            await dbContext.SaveChangesAsync();

            logger.LogDebug("Request completed {Update}", nameof(Update));

            return new operationCompleteModel() { Success = true, Id = obj.Id.ToString() };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {Update}", nameof(Update));

            var errorRetModel = new operationCompleteModel() { Success = false };
            errorRetModel.Exceptions.Add(
                new apiException() { Message = ex.Message, StatusCode = 500 }
            );
            return errorRetModel;
        }
    }

    public override async Task<responseModel> Delete(
        entityRequest request,
        ServerCallContext context
    )
    {
        try
        {
            logger.LogDebug("New Request received on {Delete}", nameof(Delete));
            var obj = await dbContext.SampleEntities.SingleAsync(x =>
                x.Id == Guid.Parse(request.Id)
            );

            dbContext.SampleEntities.Remove(obj);
            await dbContext.SaveChangesAsync();

            logger.LogDebug("Request completed {Delete}", nameof(Delete));

            return new responseModel() { Success = true };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred at {Delete}", nameof(Delete));

            var errorRetModel = new responseModel() { Success = false };
            errorRetModel.Exceptions.Add(
                new apiException() { Message = ex.Message, StatusCode = 500 }
            );
            return errorRetModel;
        }
    }
}
