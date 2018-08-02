using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    [UpdateBefore(typeof(EquipSystem))]
    public class InventorySystem : ComponentSystem
    {
        private struct InventoryData
        {
            public ComponentArray<InventoryComponent> InventoryComponents;
            public int Length;
        }

        [Inject]
        private InventoryData InventoryEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < InventoryEntities.Length; i++)
            {
                InventoryComponent inventory = InventoryEntities.InventoryComponents[i];

                inventory.dirty = false;
                while (inventory.commands.Count > 0)
                {
                    inventory.commands[0].Execute(inventory, EntityManager);
                    inventory.commands.RemoveAt(0);
                    inventory.dirty = true;
                }
            }
        }

        public interface InventoryCommand
        {
            void Execute(InventoryComponent inv, EntityManager em);
        }

        public class AddCommand : InventoryCommand
        {
            Entity item;

            public AddCommand(Entity i)
            {
                item = i;
            }

            public void Execute(InventoryComponent inv, EntityManager em)
            {
                if (em.GetComponentObject<ItemComponent>(item).equippable)
                {
                    if (inv.inventory[1] != Entity.Null) // Something already in slot
                    {
                        new DropCommand(1).Execute(inv, em);
                    }

                    inv.inventory[1] = item;
                    // TODO: Next equippable slot
                }
                else
                {
                    inv.inventory[2] = item;
                    // TODO: Next non-equippable slot

                    if (em.HasComponent<MeshRenderer>(item)) { em.GetComponentObject<MeshRenderer>(item).enabled = false; }
                }

                if (em.HasComponent<Rigidbody>(item)) { em.GetComponentObject<Rigidbody>(item).useGravity = false; }
                if (em.GetComponentObject<Transform>(item).GetComponent<Collider>() != null) { em.GetComponentObject<Transform>(item).GetComponent<Collider>().enabled = false; }
            }
        }

        public class DropCommand : InventoryCommand
        {
            int index;

            public DropCommand(int i)
            {
                index = i;
            }

            public void Execute(InventoryComponent inv, EntityManager em)
            {
                Entity item = inv.inventory[index];

                em.GetComponentObject<ItemComponent>(item).pickupCooldown = 0;
                
                if (em.HasComponent<MeshRenderer>(item)) { em.GetComponentObject<MeshRenderer>(item).enabled = true; }
                if (em.GetComponentObject<Transform>(item).GetComponent<Collider>() != null) { em.GetComponentObject<Transform>(item).GetComponent<Collider>().enabled = true; }
                if (em.HasComponent<Rigidbody>(item)) { em.GetComponentObject<Rigidbody>(item).useGravity = true; }

                inv.inventory[index] = Entity.Null;
            }
        }
    }
}
