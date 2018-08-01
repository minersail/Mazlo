using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    // Live-updates equipment slots based on inventory
    public class EquipSystem : ComponentSystem
    {
        private struct EquipData
        {
            public ComponentArray<InventoryComponent> InventoryComponents;
            public ComponentArray<EquipComponent> EquipComponents;
            public int Length;
        }

        [Inject]
        private EquipData EquipEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < EquipEntities.Length; i++)
            {
                InventoryComponent inventory = EquipEntities.InventoryComponents[i];
                EquipComponent equipment = EquipEntities.EquipComponents[i];

                if (!inventory.dirty) { return; }

                int length = Mathf.Min(inventory.inventory.Count, equipment.equipSlots.Count);

                // Indexing for inventory should correspond with equipment slots
                for (int j = 0; j < length; j++)
                {
                    if (inventory.inventory[j] != null)
                    {
                        // Drop any previously equipped items
                        for (int x = 0; x < equipment.equipSlots[j].childCount; x++)
                        {
                            equipment.equipSlots[j].GetChild(x).SetParent(null);
                        }

                        inventory.inventory[j].transform.SetParent(equipment.equipSlots[j]);
                        inventory.inventory[j].transform.localPosition = Vector3.zero;
                    }
                    else // If item slot is null item is dropped
                    {
                        for (int x = 0; x < equipment.equipSlots[j].childCount; x++)
                        {
                            equipment.equipSlots[j].GetChild(x).SetParent(null);
                        }
                    }
                }

                inventory.dirty = false;
            }
        }
    }
}
