using Sample.Service.One.GRPCClient;
using ServiceTwoProto;

namespace Sample.Service.One;

public static partial class ServicesExtensions
{
    public static IServiceCollection AddSampleGRPCClient(this IServiceCollection services, IConfiguration configuration)
    {

        var endpoint = configuration.GetSection("SampleGRPC:Endpoint").Value ?? throw new MissingFieldException("SampleGRPC:Endpoint");


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
