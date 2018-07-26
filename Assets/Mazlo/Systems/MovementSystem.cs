using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
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

                // Don't use movement if root motion is enabled
                if (EntityManager.HasComponent<Animator>(curr) && EntityManager.GetComponentObject<Animator>(curr).applyRootMotion)
                {
                    continue;
                }

                Transform trans = MovementEntities.Transforms[i];
                VelocityComponent vc = MovementEntities.VelocityComponents[i];

                trans.Translate(trans.rotation * new Vector3(vc.velocityX, 0, vc.velocityY) * vc.maxSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
