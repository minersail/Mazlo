using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mazlo.Components
{
    public class ItemComponent : MonoBehaviour
    {
        public Sprite itemSprite;
        public bool equippable;

        [HideInInspector] public float pickupCooldown;
    }
}
