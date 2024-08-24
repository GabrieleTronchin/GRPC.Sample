using Sample.GRPC.Client.API.SampleCrudService;
using SampleComunicationServiceProto;
using SampleServiceProto;

namespace Sample.GRPC.Client.API;

public static partial class ServicesExtensions
{
    public static IServiceCollection AddSampleGRPCClient(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var endpoint =
            configuration.GetSection("SampleGRPC:Endpoint").Value
            ?? throw new MissingFieldException("SampleGRPC:Endpoint");

        services
            .AddGrpcClient<SampleCrudServiceApi.SampleCrudServiceApiClient>(
                (services, options) =>
                {
                    options.Address = new Uri(endpoint);
                }
            )
            .ConfigureChannel(o =>
            {
                o.HttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                };
            });

        services
            .AddGrpcClient<SampleComunicationServiceApi.SampleComunicationServiceApiClient>(
                (services, options) =>
                {
                    options.Address = new Uri(endpoint);
                }
            )
            .ConfigureChannel(o =>
            {
                o.HttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                };
            });

        services.AddTransient<ISampleCrudServiceClientGrpc, SampleCrudServiceClientGrpc>();
        services.AddTransient<ISampleComunicationService, SampleComunicationService>();

        return services;
    }
}
