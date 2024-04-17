//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Polly.Wrapper;
//using ShowTimeProto;

//namespace Cinema.Client;

//public static partial class ServicesExtensions
//{
//    public static IServiceCollection AddCinemaClient(this IServiceCollection services, IConfiguration configuration)
//    {

//        var endpoint = configuration.GetSection("CinemaGrpc:Endpoint").Value ?? throw new MissingFieldException("CinemaGrpc:Endpoint");


//        services.AddGrpcClient<ShowTimeApi.ShowTimeApiClient>((services, options) =>
//        {
//            options.Address = new Uri(endpoint);
//        }).ConfigureChannel(o =>
//        {
//            o.HttpHandler = new HttpClientHandler
//            {
//                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
//            };
//        }).AddPolicyHandler(PollySettings.DefaultRetryPolicy());


//        services.AddTransient<IServiceTwoClientGrpc, ServiceTwoClientGrpc>();
//        return services;
//    }
//}
