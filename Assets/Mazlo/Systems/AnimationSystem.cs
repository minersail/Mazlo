using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class AnimationSystem : ComponentSystem
    {
        private struct AnimationData
        {
            public ComponentArray<Animator> Animators;
            public ComponentArray<VelocityComponent> VelocityComponents;
            public EntityArray Entities;
            public int Length;
        }

        [Inject]
        private AnimationData AnimationEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < AnimationEntities.Length; i++)
            {
                Entity curr = AnimationEntities.Entities[i];
                Animator anim = AnimationEntities.Animators[i];
                VelocityComponent vc = AnimationEntities.VelocityComponents[i];

                anim.SetFloat("VelocityX", vc.velocityX);
                anim.SetFloat("VelocityZ", vc.velocityY);

                if (EntityManager.HasComponent<AttackComponent>(curr))
                {
                    if (EntityManager.GetComponentObject<AttackComponent>(curr).attackTriggered)
                    {
                        anim.SetTrigger("Attack");
                    }
                }
            }
        }
    }
}
