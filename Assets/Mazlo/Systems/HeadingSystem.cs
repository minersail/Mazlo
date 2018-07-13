using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class HeadingSystem : ComponentSystem
    {
        private struct HeadingEntity
        {
            public Transform trans;
            public HeadingComponent heading;
        }

        protected override void OnUpdate()
        {
            foreach (HeadingEntity entity in GetEntities<HeadingEntity>())
            {
                entity.trans.Rotate(new Vector3(0, entity.heading.lookX, 0) * entity.heading.angularSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
