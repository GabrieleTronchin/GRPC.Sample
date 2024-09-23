using Grpc.Core;
using Sample.GRPC.Client.API.Models;
using SampleComunicationServiceProto;

namespace Sample.GRPC.Client.API.SampleCrudService
{
    public interface ISampleComunicationService
    {
        Task StreamingBothWays(
            IAsyncStreamReader<entitiesRequest> requestStream,
            IServerStreamWriter<entityModel> responseStream,
            ServerCallContext context
        );
        Task<responseEntitiesModel> StreamingFromClient(
            IAsyncStreamReader<entityCreationRequest> requestStream,
            ServerCallContext context
        );
        Task StreamingFromServer(
            IEnumerable<SampleEntityPost> request,
            CancellationToken cancellationToken
        );
        Task<Guid> UnaryCall(SampleEntityPost request);
    }
}
