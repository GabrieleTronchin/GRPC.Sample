using Mapster;
using Sample.GRPC.Client.API.Models;
using SampleServiceProto;

namespace Sample.GRPC.Client.API.Mapster;

public class MapsterSettings
{
    public static void Configure()
    {
        TypeAdapterConfig<entityModel, SampleEntityGet>.NewConfig()
        .Map(dest => dest.Id, src => Guid.Parse(src.Id))
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.ReferenceDate, src => src.ReferenceDate.ToDateTime())
        .Map(dest => dest.LastTimeModified, src => src.ModifiedDate.ToDateTime()); 


        TypeAdapterConfig<SampleEntityPost, CreationRequest>.NewConfig()
        .Map(dest => dest.Item.Name, src => src.Name)
        .Map(dest => dest.Item.Description, src => src.Description)
        .Map(dest => dest.Item.ReferenceDate, src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src.ReferenceDate));

        TypeAdapterConfig<SampleEntityPut, UpdateRequest>.NewConfig()
        .Map(dest => dest.Item.Id, src => src.Id)
        .Map(dest => dest.Item.Name, src => src.Name)
        .Map(dest => dest.Item.Description, src => src.Description)
        .Map(dest => dest.Item.ReferenceDate, src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src.ReferenceDate));

    }
}