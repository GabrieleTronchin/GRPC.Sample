using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.GRPC.Server.API.Persistence;

namespace Sample.GRPC.Server.API.Endpoints;

public static partial class EndpointExtensions
{
    public static void AddCRUDEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/CRUD").WithTags("CRUD Endpoints");

        group
            .MapGet(
                "/Get",
                async (DummyContext dbContext) =>
                {
                    return await dbContext.SampleEntities.ToListAsync();
                }
            )
            .WithName("Get")
            .WithOpenApi();

        group
            .MapGet(
                "/GetSingle/{payload}",
                async (DummyContext dbContext, [FromRoute] Guid payload) =>
                {
                    return await dbContext.SampleEntities.SingleAsync(x => x.Id == payload);
                }
            )
            .WithName("GetSingle")
            .WithOpenApi();


    }
}
