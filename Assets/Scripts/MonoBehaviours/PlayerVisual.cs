using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerVisual : MonoBehaviour
{
    private Entity targetEntity;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetEntity = GetRandomEntity();
        }

        if (targetEntity != Entity.Null)
        {
            Vector3 followPosition = 
                World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalToWorld>(targetEntity).Position;
            transform.position = followPosition;

        }
    }

    private Entity GetRandomEntity()
    {
        EntityQuery playerTagEntityQuery =
            World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(PlayerTag));
        NativeArray<Entity> entities = playerTagEntityQuery.ToEntityArray(Unity.Collections.Allocator.Temp);

        if (entities.Length > 0)
        {
            return entities[Random.Range(0, entities.Length)];
        }
        else
        {
            return Entity.Null;
        }
    }
}
