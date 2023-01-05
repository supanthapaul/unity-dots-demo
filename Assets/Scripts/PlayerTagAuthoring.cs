using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerTagAuthoring : MonoBehaviour { }

public class PlayerTagAuthoringBaker : Baker<PlayerTagAuthoring>
{
    public override void Bake(PlayerTagAuthoring authoring)
    {
        AddComponent(new PlayerTag());
    }
}
