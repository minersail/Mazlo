using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class ItemSystem : ComponentSystem
    {
        private struct ItemData
        {
            public ComponentArray<ItemComponent> ItemComponents;
            public int Length;
        }

        [Inject]
        private ItemData ItemEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < ItemEntities.Length; i++)
            {
                ItemComponent item = ItemEntities.ItemComponents[i];

                item.pickupCooldown += Time.deltaTime;
            }
        }
    }
}
