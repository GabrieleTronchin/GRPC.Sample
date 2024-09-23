namespace Sample.GRPC.Server.API.Endpoints;

public static partial class EndpointExtensions
{
    public static void AddComunicationEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/Comunication").WithTags("Comunication Endpoints");
        //TODO
    }
}
