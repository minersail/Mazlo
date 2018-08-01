using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Mazlo.Components
{
    public class InventoryComponent : MonoBehaviour
    {
        public readonly int INVENTORY_SIZE = 18;

        [HideInInspector] public Dictionary<int, ItemComponent> inventory = new Dictionary<int, ItemComponent>();

        public InventoryComponent()
        {
            for (int i = 0; i < INVENTORY_SIZE; i++)
            {
                inventory.Add(i, null);
            }
        }
    }
}
