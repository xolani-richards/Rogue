// using System;

// public class GatherStrategy : ILeafStrategy
// {
//     Entity entity;
//     Func<Resource> func;
//     Resource resource;
//     bool inUse;

//     public GatherStrategy(Entity entity, Func<Resource> func)
//     {
//         this.entity = entity;
//         this.func = func;
//     }
    
//     public void OnEnter()
//     {
//         resource = func();
//         inUse = resource.Use(entity);
//     }

//     public void OnLeave()
//     {
//         resource.Release(entity);
//     }

//     public Node.Status OnProcess(float deltaTime)
//     {
//         if(!inUse) return Node.Status.FAILED;
//         if(resource.isCompleted(entity)) return Node.Status.SUCCESS;
//         return Node.Status.RUNNING;
        
//     }
// }