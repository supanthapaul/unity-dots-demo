using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerSpawnerAuthoring : MonoBehaviour
{
    public GameObject playerPrefab;
    public int spawnAmount = 20;
}

public class PlayerSpawnerBaker : Baker<PlayerSpawnerAuthoring>
{
    public override void Bake(PlayerSpawnerAuthoring authoring)
    {
        AddComponent(new PlayerSpawnerComponent
        {
            playerPrefab = GetEntity(authoring.playerPrefab),
            spawnAmount = authoring.spawnAmount
        });
    }
}
