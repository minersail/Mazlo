using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class AttackSystem : ComponentSystem
    {
        private struct AttackData
        {
            public ComponentArray<AttackComponent> AttackComponents;
            public EntityArray Entities;
            public int Length;
        }

        [Inject]
        private AttackData AttackEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < AttackEntities.Length; i++)
            {
                Debug.Log("Attacking");
            }
        }
    }
}
