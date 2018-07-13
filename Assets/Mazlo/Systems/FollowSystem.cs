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
            public InputComponent input;
        }

        protected override void OnUpdate()
        {
            foreach (FollowerEntity entity in GetEntities<FollowerEntity>())
            {
                Vector3 direction = (entity.fc.followTrans.position + entity.fc.offset) - entity.trans.position;

                direction.Normalize();

                entity.input.moveX = direction.x;
                entity.input.moveY = direction.z;
            }
        }
    }
}
