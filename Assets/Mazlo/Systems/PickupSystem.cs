using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    [UpdateBefore(typeof(InventorySystem))]
    [UpdateBefore(typeof(EquipSystem))]
    public class PickupSystem : ComponentSystem
    {
        private struct PickupData
        {
            public ComponentArray<PickupComponent> PickupComponents;
            public ComponentArray<TriggerComponent> TriggerComponents;
            public ComponentArray<InventoryComponent> InventoryComponents;
            public EntityArray Entities;
            public int Length;
        }

        [Inject]
        private PickupData PickupEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < PickupEntities.Length; i++)
            {
                PickupComponent pickup = PickupEntities.PickupComponents[i];
                TriggerComponent trigger = PickupEntities.TriggerComponents[i];
                InventoryComponent inventory = PickupEntities.InventoryComponents[i];
                Entity curr = PickupEntities.Entities[i];

                if (pickup.isPickingUp)
                {
                    foreach (TriggerComponent.TriggerData data in trigger.triggers)
                    {
                        // Can only pick up items
                        if (data.entity != Entity.Null && EntityManager.HasComponent<ItemComponent>(data.entity))
                        {
                            ItemComponent item = EntityManager.GetComponentObject<ItemComponent>(data.entity);

                            if (item.equippable)
                            {
                                inventory.inventory[1] = item;
                                // TODO: Next equippable slot
                            }
                            else
                            {
                                inventory.inventory[2] = item;
                                // TODO: Next non-equippable slot
                            }

                            PickUpEntity(data.entity, item.equippable);
                            inventory.dirty = true;
                        }
                    }
                }
            }
        }

        private void PickUpEntity(Entity en, bool equippable)
        {
            if (!equippable)
            {
                if (EntityManager.HasComponent<MeshRenderer>(en))
                {
                    EntityManager.GetComponentObject<MeshRenderer>(en).enabled = false;
                }

                if (EntityManager.GetComponentObject<Transform>(en).GetComponent<Collider>() != null)
                {
                    EntityManager.GetComponentObject<Transform>(en).GetComponent<Collider>().enabled = false;
                }
            }

            if (EntityManager.HasComponent<Rigidbody>(en))
            {
                EntityManager.GetComponentObject<Rigidbody>(en).useGravity = false;
            }
        }
    }
}
