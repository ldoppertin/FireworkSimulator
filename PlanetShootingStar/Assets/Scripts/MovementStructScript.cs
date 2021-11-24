using Unity.Entities;
using System;

[GenerateAuthoringComponent]
public struct Movement : IComponentData
{
    public float speed;
}

