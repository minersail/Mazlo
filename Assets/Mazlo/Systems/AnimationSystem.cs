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
            public VelocityComponent movement;
        }

        protected override void OnUpdate()
        {
            foreach (AnimationEntity entity in GetEntities<AnimationEntity>())
            {
                entity.animator.SetFloat("VelocityX", entity.movement.velocityX);
                entity.animator.SetFloat("VelocityZ", entity.movement.velocityY);
            }
        }
    }
}
