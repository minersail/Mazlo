using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Entities;
using Mazlo.Components;

public class HandSlots : MonoBehaviour
{
    public Image leftItem;
    public Image rightItem;
    public Sprite empty;

    public InventoryComponent IC;
	
	void Update ()
    {
        if (IC.inventory[0] == Entity.Null)
        {
            leftItem.sprite = empty;
        }
        else
        {
            leftItem.sprite = 
                World.Active.GetExistingManager<EntityManager>().GetComponentObject<ItemComponent>(IC.inventory[0]).itemSprite;
        }

        if (IC.inventory[1] == Entity.Null)
        {
            rightItem.sprite = empty;
        }
        else
        {
            rightItem.sprite =
                World.Active.GetExistingManager<EntityManager>().GetComponentObject<ItemComponent>(IC.inventory[1]).itemSprite;
        }
    }
}
