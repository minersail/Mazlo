using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Mazlo.Components
{
    public class TriggerComponent : MonoBehaviour
    {
        [HideInInspector] public List<TriggerData> triggers = new List<TriggerData>();
        
        /// <summary>
        /// Represents an object that this entity has collided with.
        /// If the object is not an entity, the entity field will be null.
        /// </summary>
        public class TriggerData
        {
            public Entity entity;
            public Collider trigger;

            public TriggerData(Collider c) : this(c, Entity.Null) {}

            public TriggerData(Collider c, Entity e)
            {
                trigger = c;
                entity = e;
            }
        }
    }
}
