// using System;

// public class ReserveSmartObjectStrategy : ILeafStrategy
// {
//     Entity entity;
//     Func<Resource> func;
//     Resource resource;

//     public ReserveSmartObjectStrategy(Entity entity, Func<Resource> func)
//     {
//         this.entity = entity;
//         this.func = func;
//     }
    
//     public void OnEnter()
//     {
//         resource = func();
//     }

//     public void OnLeave()
//     {
//     }

//     public Node.Status OnProcess(float deltaTime)
//     {
//         if(resource == null) return Node.Status.FAILED;
//         return resource.Reserve(entity) ? Node.Status.SUCCESS: Node.Status.FAILED;
//     }
// }