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

                if (EntityManager.HasComponent<VelocityComponent>(curr))
                {
                    VelocityComponent vc = EntityManager.GetComponentObject<VelocityComponent>(curr);

                    if (AnimationEnded(anim, "Pickup") || AnimationEnded(anim, "Attack"))
                    {
                        vc.movementLocked = false;
                    }

                    if (!vc.movementLocked)
                    {
                        anim.SetFloat("VelocityX", vc.inputX * vc.movementMultiplier);
                        anim.SetFloat("VelocityZ", vc.inputY * vc.movementMultiplier);
                    }
                }

                if (EntityManager.HasComponent<AttackComponent>(curr))
                {
                    if (EntityManager.GetComponentObject<AttackComponent>(curr).attackTriggered)
                        anim.SetTrigger("Attack");
                    else
                        anim.ResetTrigger("Attack");
                }

                if (EntityManager.HasComponent<PickupComponent>(curr))
                {
                    if (EntityManager.GetComponentObject<PickupComponent>(curr).pickupTriggered)
                        anim.SetTrigger("Pickup");
                    else
                        anim.ResetTrigger("Pickup");
                }
            }
        }

        private bool AnimationEnded(Animator anim, string animationName)
        {
            return anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName(animationName);
        }
    }
}
