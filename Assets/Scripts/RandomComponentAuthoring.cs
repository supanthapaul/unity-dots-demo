using Unity.Entities;
using UnityEngine;


public class RandomComponentAuthoring : MonoBehaviour { }

public class RandomBaker : Baker<RandomComponentAuthoring>
{
    public override void Bake(RandomComponentAuthoring authoring)
    {
        AddComponent(new RandomComponent
        {
            random = new Unity.Mathematics.Random(1)
        });
    }
}