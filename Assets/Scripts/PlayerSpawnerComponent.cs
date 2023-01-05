using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct PlayerSpawnerComponent : IComponentData
{
    public Entity playerPrefab;
    public int spawnAmount;
}
