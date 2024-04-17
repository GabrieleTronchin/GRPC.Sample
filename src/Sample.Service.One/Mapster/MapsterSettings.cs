using Mapster;
using Sample.GRPC.Client.API.Models;
using SampleServiceProto;

namespace Sample.GRPC.Client.API.Mapster;

public class MapsterSettings
{
    public static void Configure()
    {
        TypeAdapterConfig<entityModel, SampleEntityGet>.NewConfig()
        .Map(dest => dest.ReferenceDate, src => src.ReferenceDate.ToDateTime());

        TypeAdapterConfig<SampleEntityPost, CreationRequest>.NewConfig()
        .Map(dest => dest.Item.ReferenceDate, src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src.ReferenceDate));

        TypeAdapterConfig<SampleEntityPut, UpdateRequest>.NewConfig()
        .Map(dest => dest.Item.ReferenceDate, src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src.ReferenceDate));

    }
}