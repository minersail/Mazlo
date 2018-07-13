using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class AnimationSystem : ComponentSystem
    {
        private struct AnimationEntity
        {
            public Animator animator;
            public InputComponent input;
        }

        protected override void OnUpdate()
        {
            foreach (AnimationEntity entity in GetEntities<AnimationEntity>())
            {
                // Disable root motion if motion is controlled manually instead
                if (entity.input.GetComponent<VelocityComponent>() != null) entity.animator.applyRootMotion = false;

                entity.animator.SetFloat("VelocityX", entity.input.moveX);
                entity.animator.SetFloat("VelocityZ", entity.input.moveY);
            }
        }
    }
}
