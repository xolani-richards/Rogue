// using System;
// using Goblins.Items;

// public class DepositItemsStrategy : ILeafStrategy
// {
//     Entity entity;
//     Func<Item> getItem;
//     Func<Storage> getStorage;
//     Inventory inventory;
//     Item item;
//     Storage storage;
//     int amount;

//     public DepositItemsStrategy(Entity entity, Func<Item> getItem, Func<Storage> getStorage, int amount = 1)
//     {
//         this.entity = entity;
//         this.getItem = getItem;
//         this.getStorage = getStorage;
//         this.amount = amount;
//     }

//     public void OnEnter()
//     {
//         item = getItem();
//         storage = getStorage();
//         inventory = entity.GetComponent<Inventory>();
//     }

//     public void OnLeave()
//     {
//     }

//     public Node.Status OnProcess(float deltaTime)
//     {
//         if(inventory == null || item == null || storage == null) return Node.Status.FAILED;
//         if(!inventory.hasItems(item, amount)) return Node.Status.FAILED;
//         storage.AddItems(item, amount);
//         inventory.RemoveItems(item, amount);
//         return Node.Status.SUCCESS;
//     }
// }