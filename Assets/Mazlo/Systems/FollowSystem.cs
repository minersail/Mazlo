using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class FollowSystem : ComponentSystem
    {
        private struct FollowerEntity
        {
            public Transform trans;
            public FollowComponent fc;
            public VelocityComponent input;
        }

        protected override void OnUpdate()
        {
            foreach (FollowerEntity entity in GetEntities<FollowerEntity>())
            {
                Vector3 direction = entity.fc.followTrans.position - entity.trans.position;

                if (direction.magnitude < entity.fc.stopDistance)
                {
                    entity.input.velocityX = Mathf.Lerp(entity.input.velocityX, 0, Time.deltaTime * 3);
                    entity.input.velocityY = Mathf.Lerp(entity.input.velocityY, 0, Time.deltaTime * 3);
                }
                else
                {
                    direction.Normalize();

                    entity.input.velocityX = Mathf.Lerp(entity.input.velocityX, direction.x, Time.deltaTime * 3);
                    entity.input.velocityY = Mathf.Lerp(entity.input.velocityY, direction.z, Time.deltaTime * 3);
                }
            }
        }
    }
}
