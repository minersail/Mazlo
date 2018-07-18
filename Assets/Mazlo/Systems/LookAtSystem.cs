using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class LookAtSystem : ComponentSystem
    {
        private struct FollowerEntity
        {
            public Transform trans;
            public LookAtComponent lookAt;
            public HeadingComponent heading;
        }

        protected override void OnUpdate()
        {
            foreach (FollowerEntity entity in GetEntities<FollowerEntity>())
            {
                Vector3 direction = entity.lookAt.lookTrans.position - entity.trans.position;

                entity.trans.rotation = Quaternion.Lerp(entity.trans.rotation, Quaternion.LookRotation(direction), Time.deltaTime * entity.heading.angularSpeed);
            }
        }
    }
}
