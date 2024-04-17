//using Grpc.Core;
//using ShowTimeProto;

//namespace Cinema.Client;

//public class ServiceTwoClientGrpc : IServiceTwoClientGrpc
//{
//    private readonly ShowTimeApi.ShowTimeApiClient _client;
//    private readonly Metadata _metadata;

//    public ServiceTwoClientGrpc(ShowTimeApi.ShowTimeApiClient client)
//    {
//        _client = client;
//        _metadata = new Metadata
//        {
//            { "X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63" } //just for test purpose
//        };
//    }
//    public async Task<responseModel> CreateShowTime(ShowtimeCreationRequest request)
//    {
//        return await _client.CreateShowTimeAsync(request, _metadata);
//    }

//}