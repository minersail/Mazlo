using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class HeadingSystem : ComponentSystem
    {
        private struct HeadingData
        {
            public ComponentArray<Transform> Transforms;
            public ComponentArray<HeadingComponent> HeadingComponents;
            public int Length;
        }

        [Inject]
        private HeadingData HeadingEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < HeadingEntities.Length; i++)
            {
                Transform trans = HeadingEntities.Transforms[i];
                HeadingComponent heading = HeadingEntities.HeadingComponents[i];

                trans.Rotate(new Vector3(0, heading.lookX, 0) * heading.angularSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
