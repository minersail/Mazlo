using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class MovementSystem : ComponentSystem
    {
        private struct MovementEntity
        {
            public Transform trans;
            public VelocityComponent vc;
        }

        protected override void OnUpdate()
        {
            foreach (MovementEntity entity in GetEntities<MovementEntity>())
            {
                // Don't use movement if root motion is enabled
                if (entity.trans.GetComponent<Animator>().applyRootMotion) { continue; }

                entity.trans.Translate(entity.trans.rotation * new Vector3(entity.vc.velocityX, 0, entity.vc.velocityY) * entity.vc.maxSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
