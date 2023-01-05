using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public partial struct MovingISystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state) { }

    [BurstCompile]
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // get REFERENCE to a singleton instance
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        float deltaTime = SystemAPI.Time.DeltaTime;
        
        // Schedule this job along multiple worker threads (ScheduleParallel)
        // (Each worker thread will deal with a certain amount of entities)
        JobHandle jobHandle = new MoveJob
        {
            deltaTime = deltaTime
        }.ScheduleParallel(state.Dependency);
        
        // wait for this job to finish
        jobHandle.Complete();
        
        // Run this job on the main thread (As we are dealing with a reference here)
        // Running it in parallel can cause race conditions
        new TestReachedDestinationJob
        {
            randomComponent = randomComponent
        }.Run();
    }
}

[BurstCompile]
public partial struct MoveJob : IJobEntity
{
    public float deltaTime;
    
    [BurstCompile]
    public void Execute(MoveToPositionAspect moveToPositionAspect)
    {
        moveToPositionAspect.Move(deltaTime);
    }
}

[BurstCompile]
public partial struct TestReachedDestinationJob : IJobEntity
{
    [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent;
    
    [BurstCompile]
    public void Execute(MoveToPositionAspect moveToPositionAspect)
    {
        moveToPositionAspect.TestReachedDestination(randomComponent);
    }
}
