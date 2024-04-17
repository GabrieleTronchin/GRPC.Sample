using ServiceTwoProto;

namespace Sample.Service.One.GRPCClient;

public static partial class ServicesExtensions
{
    public static IServiceCollection AddCinemaClient(this IServiceCollection services, IConfiguration configuration)
    {

        var endpoint = configuration.GetSection("CinemaGrpc:Endpoint").Value ?? throw new MissingFieldException("CinemaGrpc:Endpoint");


        services.AddGrpcClient<ServiceTwoApi.ServiceTwoApiClient>((services, options) =>
        {
            options.Address = new Uri(endpoint);
        }).ConfigureChannel(o =>
        {
            o.HttpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
        });


        services.AddTransient<IServiceTwoClientGrpc, ServiceTwoClientGrpc>();
        return services;
    }
}
