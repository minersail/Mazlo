using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    [UpdateBefore(typeof(InventorySystem))]
    public class PickupSystem : ComponentSystem
    {
        public const float PICKUP_COOLDOWN = 1.0f;

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
                        if (data.entity != Entity.Null && EntityManager.HasComponent<ItemComponent>(data.entity) && 
                            EntityManager.GetComponentObject<ItemComponent>(data.entity).pickupCooldown > PICKUP_COOLDOWN) // Make sure items have a small wait time before being picked up again
                        {
                            inventory.commands.Add(new InventorySystem.AddCommand(data.entity));
                            trigger.triggers.Remove(data);
                            break;
                        }
                    }
                }
            }
        }
    }
}
