using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    [UpdateBefore(typeof(AnimationSystem))]
    [UpdateBefore(typeof(EnergySystem))]
    [UpdateAfter(typeof(FollowSystem))]
    public class MovementSystem : ComponentSystem
    {
        private struct MovementData
        {
            public ComponentArray<Transform> Transforms;
            public ComponentArray<VelocityComponent> VelocityComponents;
            public EntityArray Entities;
            public int Length;
        }

        [Inject]
        private MovementData MovementEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < MovementEntities.Length; i++)
            {
                Entity curr = MovementEntities.Entities[i];
                Transform trans = MovementEntities.Transforms[i];
                VelocityComponent vc = MovementEntities.VelocityComponents[i];

                if (vc.movementLocked)
                {
                    if (EntityManager.HasComponent<EnergyComponent>(curr))
                    {
                        EntityManager.GetComponentObject<EnergyComponent>(curr).regenEnabled = true;
                    }
                    continue;
                }

                // Don't use movement if root motion is enabled
                if (EntityManager.HasComponent<Animator>(curr) && EntityManager.GetComponentObject<Animator>(curr).applyRootMotion)
                {
                    continue;
                }

                Vector3 movement = GetMovementVector(vc);

                if (EntityManager.HasComponent<EnergyComponent>(curr))
                {
                    if (EntityManager.GetComponentObject<EnergyComponent>(curr).energy <= 0)
                    {
                        vc.movementMultiplier = Mathf.Lerp(vc.movementMultiplier, 0.5f, Time.deltaTime);
                    }
                    else
                    {
                        vc.movementMultiplier = Mathf.Lerp(vc.movementMultiplier, 1, Time.deltaTime);
                    }

                    float distTravelled = movement.magnitude;
                    EntityManager.GetComponentObject<EnergyComponent>(curr).energy -= distTravelled;
                    EntityManager.GetComponentObject<EnergyComponent>(curr).regenEnabled = distTravelled == 0;
                }

                trans.Translate(trans.rotation * movement * vc.movementMultiplier, Space.World);
            }
        }

        private Vector3 GetMovementVector(VelocityComponent vc)
        {
            // normalize then multiply by higher input
            float norm = Mathf.Max(Mathf.Abs(vc.inputX), Mathf.Abs(vc.inputY));

            Vector3 movement = new Vector3(vc.inputX, 0, vc.inputY);
            movement.Normalize();
            movement *= norm;

            return movement * vc.maxSpeed * Time.deltaTime;
        }
    }
}
