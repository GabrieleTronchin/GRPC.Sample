namespace Sample.GRPC.Client.API.Models;


public class SampleEntityPost
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReferenceDate { get; set; }
}


public class SampleEntityPut : SampleEntityPost
{
    public Guid Id { get; set; }

}



public class SampleEntityGet : SampleEntityPut
{
    public DateTime LastTimeModified { get; set; }
}
