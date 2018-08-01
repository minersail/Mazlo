using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Systems;

namespace Mazlo.Components
{
    public class InventoryComponent : MonoBehaviour
    {
        public readonly int INVENTORY_SIZE = 18;

        [HideInInspector] public Dictionary<int, Entity> inventory = new Dictionary<int, Entity>();
        [HideInInspector] public List<InventorySystem.InventoryCommand> commands = new List<InventorySystem.InventoryCommand>();
        [HideInInspector] public bool dirty;

        public InventoryComponent()
        {
            for (int i = 0; i < INVENTORY_SIZE; i++)
            {
                inventory.Add(i, Entity.Null);
            }
        }
    }
}
