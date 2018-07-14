using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class AttackSystem : ComponentSystem
    {
        private struct AttackEntity
        {
            public AttackComponent attack;
            public Animator anim;
        }

        protected override void OnUpdate()
        {
            foreach (AttackEntity entity in GetEntities<AttackEntity>())
            {
                if (entity.attack.attackTriggered)
                {
                    entity.anim.SetTrigger("Attack");
                }
            }
        }
    }
}
