using Microsoft.AspNetCore.Mvc;
using Sample.GRPC.Client.API.Models;
using Sample.GRPC.Client.API.SampleCrudService;

namespace Sample.GRPC.Client.API.Endpoints;

public static partial class EndpointExtensions
{
    public static void AddComunicationEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/Comunication").WithTags("Comunication Endpoints");

        group
            .MapPost(
                "/UnaryCall",
                async (ISampleComunicationService service, [FromBody] SampleEntityPost payload) =>
                {
                    return await service.UnaryCall(payload);
                }
            )
            .WithName("UnaryCall")
            .WithOpenApi();

        group
            .MapPost(
                "/StreamingFromServer",
                async (ISampleComunicationService service, [FromBody] SampleEntityPost payload) =>
                {
                    //return await service.StreamingFromServer(payload);
                    throw new NotImplementedException();
                }
            )
            .WithName("StreamingFromServer")
            .WithOpenApi();

        group
            .MapPost(
                "/StreamingFromClient",
                async (ISampleComunicationService service, [FromBody] SampleEntityPost payload) =>
                {
                    //return await service.StreamingFromClient(payload);
                    throw new NotImplementedException();
                }
            )
            .WithName("StreamingFromClient")
            .WithOpenApi();

        group
            .MapPost(
                "/StreamingBothWays",
                async (ISampleComunicationService service, [FromBody] SampleEntityPost payload) =>
                {
                    //return await service.StreamingBothWays(payload);
                    throw new NotImplementedException();
                }
            )
            .WithName("StreamingBothWays")
            .WithOpenApi();
    }
}
