using Unity.Entities;
using UnityEngine;

public struct RandomComponent : IComponentData
{
    public Unity.Mathematics.Random random;
}