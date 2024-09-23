using Microsoft.AspNetCore.Mvc;
using Sample.GRPC.Client.API.Models;
using Sample.GRPC.Client.API.SampleCrudService;

namespace Sample.GRPC.Client.API.Endpoints;

public static partial class EndpointExtensions
{
    public static void AddCRUDEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/CRUD").WithTags("CRUD Endpoints");
        group
            .MapGet(
                "/Get",
                async (ISampleCrudServiceClientGrpc service) =>
                {
                    return await service.Get();
                }
            )
            .WithName("Get")
            .WithOpenApi();

        group
            .MapGet(
                "/GetSingle/{payload}",
                async (ISampleCrudServiceClientGrpc service, [FromRoute] string payload) =>
                {
                    return await service.GetSingle(payload);
                }
            )
            .WithName("GetSingle")
            .WithOpenApi();

        group
            .MapPost(
                "/Create",
                async (ISampleCrudServiceClientGrpc service, [FromBody] SampleEntityPost payload) =>
                {
                    return await service.Create(payload);
                }
            )
            .WithName("Create")
            .WithOpenApi();

        group
            .MapPut(
                "/Update",
                async (ISampleCrudServiceClientGrpc service, [FromBody] SampleEntityPut payload) =>
                {
                    return await service.Update(payload);
                }
            )
            .WithName("Update")
            .WithOpenApi();

        group
            .MapDelete(
                "/Delete/{payload}",
                async (ISampleCrudServiceClientGrpc service, [FromRoute] string payload) =>
                {
                    await service.Delete(payload);
                }
            )
            .WithName("Delete")
            .WithOpenApi();
    }
}
