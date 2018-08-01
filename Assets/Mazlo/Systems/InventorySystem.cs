using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class InventorySystem : ComponentSystem
    {
        private struct InventoryData
        {
            public ComponentArray<InventoryComponent> InventoryComponents;
            public EntityArray Entities;
            public int Length;
        }

        [Inject]
        private InventoryData InventoryEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < InventoryEntities.Length; i++)
            {
                // Some sort of drag feature - idk
            }
        }
    }
}
