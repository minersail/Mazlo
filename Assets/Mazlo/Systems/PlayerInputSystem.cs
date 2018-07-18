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
            public PlayerComponent player;
            public Transform trans;
        }

        protected override void OnUpdate()
        {
            foreach (PlayerEntity entity in GetEntities<PlayerEntity>())
            {
                if (entity.trans.GetComponent<VelocityComponent>() != null)
                {
                    entity.trans.GetComponent<VelocityComponent>().velocityX = Input.GetAxis("Horizontal");
                    entity.trans.GetComponent<VelocityComponent>().velocityY = Input.GetAxis("Vertical");
                }

                if (entity.trans.GetComponent<HeadingComponent>() != null)
                {
                    entity.trans.GetComponent<HeadingComponent>().lookX = Input.GetAxis("Mouse X");
                    entity.trans.GetComponent<HeadingComponent>().lookY = Input.GetAxis("Mouse Y");
                    Cursor.lockState = CursorLockMode.Locked;
                }

                if (entity.trans.GetComponent<AttackComponent>() != null)
                {
                    entity.trans.GetComponent<AttackComponent>().attackTriggered = Input.GetKeyDown(KeyCode.Space);
                }
            }
        }
    }
}
