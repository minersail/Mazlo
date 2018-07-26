﻿using System.Collections;
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
        private struct PlayerData
        {
            public ComponentArray<PlayerComponent> Player;
            public EntityArray Entities;
            public int Length;
        }

        [Inject]
        private PlayerData PlayerEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < PlayerEntities.Length; i++)
            {
                Entity curr = PlayerEntities.Entities[i];

                if (EntityManager.HasComponent<VelocityComponent>(curr))
                {
                    EntityManager.GetComponentObject<VelocityComponent>(curr).velocityX = Input.GetAxis("Horizontal");
                    EntityManager.GetComponentObject<VelocityComponent>(curr).velocityY = Input.GetAxis("Vertical");
                }

                if (EntityManager.HasComponent<HeadingComponent>(curr))
                {
                    EntityManager.GetComponentObject<HeadingComponent>(curr).lookX = Input.GetAxis("Mouse X");
                    EntityManager.GetComponentObject<HeadingComponent>(curr).lookY = Input.GetAxis("Mouse Y");
                }

                if (EntityManager.HasComponent<AttackComponent>(curr))
                {
                    EntityManager.GetComponentObject<AttackComponent>(curr).attackTriggered = Input.GetKeyDown(KeyCode.Space);
                }
            }
        }
    }
}
