/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class RotationSystem : ComponentSystem
    {
        private struct RotationEntity
        {
            public Transform trans;
            public AngularVelocityComponent avc;
            public InputComponent input;
        }

        protected override void OnUpdate()
        {
            foreach (RotationEntity entity in GetEntities<RotationEntity>())
            {
                if (entity.input.moveX == 0 && entity.input.moveY == 0)
                    return;

                Vector3 rotation = entity.trans.eulerAngles;

                float nonZeroX = entity.input.moveX == 0 ? 0.0000001f : -entity.input.moveX;
                float angle = Mathf.Rad2Deg * Mathf.Atan(entity.input.moveY / nonZeroX);

                // Normalize between 0, 360
                angle = nonZeroX > 0 ? angle : angle + 180;
                angle = angle > 0 ? angle : angle + 360;
                rotation.y = Mathf.LerpAngle(rotation.y, angle, Time.deltaTime * entity.avc.angularVelocity);

                entity.trans.eulerAngles = rotation;
            }
        }
    }
}
*/