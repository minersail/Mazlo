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
            public InputComponent input;
        }

        protected override void OnUpdate()
        {
            foreach (MovementEntity entity in GetEntities<MovementEntity>())
            {
                entity.trans.Translate(new Vector3(entity.input.moveX, 0, entity.input.moveY) * entity.vc.velocity * Time.deltaTime, Space.World);
            }
        }
    }
}
