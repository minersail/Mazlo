using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    [UpdateBefore(typeof(MovementSystem))]
    [UpdateBefore(typeof(HeadingSystem))]
    public class PlayerInputSystem : ComponentSystem
    {
        private struct PlayerEntity
        {
            public InputComponent input;
            public PlayerComponent player;
        }

        protected override void OnUpdate()
        {
            foreach (PlayerEntity entity in GetEntities<PlayerEntity>())
            {
                entity.input.moveX = Input.GetAxis("Horizontal");
                entity.input.moveY = Input.GetAxis("Vertical");

                entity.input.lookX = Input.GetAxis("Mouse X");
                entity.input.lookY = Input.GetAxis("Mouse Y");

                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
