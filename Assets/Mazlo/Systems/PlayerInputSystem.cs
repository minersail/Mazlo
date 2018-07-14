using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    [UpdateBefore(typeof(MovementSystem))]
    [UpdateBefore(typeof(HeadingSystem))]
    [UpdateBefore(typeof(AttackSystem))]
    public class PlayerInputSystem : ComponentSystem
    {
        private struct PlayerEntity
        {
            public VelocityComponent movement;
            public HeadingComponent heading;
            public PlayerComponent player;
            public AttackComponent attack;
        }

        protected override void OnUpdate()
        {
            foreach (PlayerEntity entity in GetEntities<PlayerEntity>())
            {
                entity.movement.velocityX = Input.GetAxis("Horizontal");
                entity.movement.velocityY = Input.GetAxis("Vertical");

                entity.heading.lookX = Input.GetAxis("Mouse X");
                entity.heading.lookY = Input.GetAxis("Mouse Y");

                entity.attack.attackTriggered = Input.GetKeyDown(KeyCode.Space);

                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
