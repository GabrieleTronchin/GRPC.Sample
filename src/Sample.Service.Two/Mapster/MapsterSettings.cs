using Mapster;
using Sample.GRPC.Server.API.Models;
using SampleServiceProto;

namespace Sample.GRPC.Server.API.Mapster;

public class MapsterSettings
{
    public static void Configure()
    {
        TypeAdapterConfig<DummyEntity, entityModel>.NewConfig()
        .Map(dest => dest.Id, src => src.Id.ToString())
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.ReferenceDate, src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src.ReferenceDate))
        .Map(dest => dest.ModifiedDate, src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src.LastTimeModified));


    }
}