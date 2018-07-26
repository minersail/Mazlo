using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    /// <summary>
    /// Handles entities that track other entities positionally
    /// </summary>
    public class FollowSystem : ComponentSystem
    {
        private struct FollowerData
        {
            public ComponentArray<Transform> Transforms;
            public ComponentArray<FollowComponent> FollowComponents;
            public ComponentArray<VelocityComponent> VelocityComponents;
            public int Length;
        }

        private struct LookAtData
        {
            public ComponentArray<Transform> Transforms;
            public ComponentArray<LookAtComponent> LookAtComponents;
            public ComponentArray<HeadingComponent> HeadingComponents;
            public int Length;
        }

        [Inject]
        private FollowerData FollowEntities;

        [Inject]
        private LookAtData LookAtEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < FollowEntities.Length; i++)
            {
                Transform trans = FollowEntities.Transforms[i];
                FollowComponent fc = FollowEntities.FollowComponents[i];
                VelocityComponent vc = FollowEntities.VelocityComponents[i];

                Vector3 direction = trans.InverseTransformDirection(fc.followTrans.position - trans.position);

                if (direction.magnitude < fc.stopDistance)
                {
                    vc.velocityX = Mathf.Lerp(vc.velocityX, 0, Time.deltaTime * 3);
                    vc.velocityY = Mathf.Lerp(vc.velocityY, 0, Time.deltaTime * 3);
                }
                else
                {
                    direction.Normalize();

                    vc.velocityX = Mathf.Lerp(vc.velocityX, direction.x, Time.deltaTime * 3);
                    vc.velocityY = Mathf.Lerp(vc.velocityY, direction.z, Time.deltaTime * 3);
                }
            }

            for (int i = 0; i < LookAtEntities.Length; i++)
            {
                Transform trans = LookAtEntities.Transforms[i];
                LookAtComponent lookAt = LookAtEntities.LookAtComponents[i];
                HeadingComponent heading = LookAtEntities.HeadingComponents[i];

                Vector3 direction = lookAt.lookTrans.position - trans.position;

                trans.rotation = Quaternion.Lerp(trans.rotation, Quaternion.LookRotation(direction), Time.deltaTime * heading.angularSpeed);
            }
        }
    }
}
