using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct MoveToPositionAspect : IAspect
{
    // Will be automatically filled with the entity that's using this aspect
    private readonly Entity entity;
    
    private readonly TransformAspect transformAspect;
    private readonly RefRO<Speed> speed;
    private readonly RefRW<TargetPosition> targetPosition;

    public void Move(float deltaTime)
    {
        // calculate direction
        float3 direction = math.normalize(targetPosition.ValueRO.value - transformAspect.LocalPosition);
        // move
        transformAspect.LocalPosition += direction * deltaTime * speed.ValueRO.value;
    }

    public void TestReachedDestination(RefRW<RandomComponent> randomComponent)
    {
        float reachedDestination = 0.5f;
        if (math.distance(targetPosition.ValueRW.value, transformAspect.LocalPosition) < reachedDestination)
        {
            targetPosition.ValueRW.value = GetRandomPosition(randomComponent);
        }
    }

    private float3 GetRandomPosition(RefRW<RandomComponent> randomComponent)
    {
        return new float3(
            randomComponent.ValueRW.random.NextFloat(-20f, 20f),
            0f,
            randomComponent.ValueRW.random.NextFloat(-20f, 20f)
        );
    }
}
