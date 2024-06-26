﻿namespace Sample.GRPC.Server.API.Models;

public class DummyEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReferenceDate { get; set; }

    public DateTime LastTimeModified { get; set; }
}
