using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class PickupSystem : ComponentSystem
    {
        private struct PickupData
        {
            public ComponentArray<PickupComponent> PickupComponents;
            public ComponentArray<TriggerComponent> TriggerComponents;
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

                if (pickup.isPickingUp)
                {
                    foreach (TriggerComponent.TriggerData data in trigger.triggers)
                    {
                        if (data.entity != Entity.Null)
                        {
                            data.trigger.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
