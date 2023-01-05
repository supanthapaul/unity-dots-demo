using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class PlayerSpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        EntityQuery playerEntityQuery = EntityManager.CreateEntityQuery(typeof(PlayerTag));

        PlayerSpawnerComponent playerSpawnerComponent = SystemAPI.GetSingleton<PlayerSpawnerComponent>();
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

        // we should spawn or remove entities in a specific command buffer
        EntityCommandBuffer entityCommandBuffer = 
            SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
        
        int spawnAmount = playerSpawnerComponent.spawnAmount;
        if (playerEntityQuery.CalculateEntityCount() < spawnAmount)
        {
            //EntityManager.Instantiate(playerSpawnerComponent.playerPrefab);
            Entity spawnedEntity = entityCommandBuffer.Instantiate(playerSpawnerComponent.playerPrefab);
            // assigning a random speed
            entityCommandBuffer.SetComponent(spawnedEntity, new Speed
            {
                value = randomComponent.ValueRW.random.NextFloat(1f, 5f)
            });
        }
    }
}
