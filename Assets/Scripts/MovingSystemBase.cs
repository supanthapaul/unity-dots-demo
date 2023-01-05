using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public partial class MovingSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
        // get REFERENCE to a singleton instance
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        // RefRO - Reference Read Only 
        // RefRW - Reference Read Write 
        // For each entity with the following components or aspects, run the code inside (Runs on the main thread)
        foreach (MoveToPositionAspect moveToPositionAspect in SystemAPI.Query<MoveToPositionAspect>())
        {
            moveToPositionAspect.Move(SystemAPI.Time.DeltaTime);
            moveToPositionAspect.TestReachedDestination(randomComponent);
        }
    }
}
