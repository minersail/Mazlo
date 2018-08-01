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
    [UpdateBefore(typeof(AnimationSystem))]
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
                    EntityManager.GetComponentObject<VelocityComponent>(curr).inputX = Input.GetAxis("Horizontal");
                    EntityManager.GetComponentObject<VelocityComponent>(curr).inputY = Input.GetAxis("Vertical");
                }

                if (EntityManager.HasComponent<HeadingComponent>(curr))
                {
                    EntityManager.GetComponentObject<HeadingComponent>(curr).lookX = Input.GetAxis("Mouse X");
                    EntityManager.GetComponentObject<HeadingComponent>(curr).lookY = Input.GetAxis("Mouse Y");
                    Cursor.lockState = CursorLockMode.Locked;
                }

                if (EntityManager.HasComponent<AttackComponent>(curr))
                {
                    bool attacking = Input.GetKeyDown(KeyCode.Space);

                    EntityManager.GetComponentObject<AttackComponent>(curr).attackTriggered = attacking;
                    if (attacking && EntityManager.HasComponent<VelocityComponent>(curr))
                    {
                        EntityManager.GetComponentObject<VelocityComponent>(curr).movementLocked = true;
                    }
                }

                if (EntityManager.HasComponent<PickupComponent>(curr))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        EntityManager.GetComponentObject<PickupComponent>(curr).isPickingUp = true;
                        if (EntityManager.HasComponent<VelocityComponent>(curr))
                        {
                            EntityManager.GetComponentObject<VelocityComponent>(curr).movementLocked = true;
                        }
                    }
                }

                if (EntityManager.HasComponent<InventoryComponent>(curr))
                {
                    InventoryComponent inv = EntityManager.GetComponentObject<InventoryComponent>(curr);

                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        inv.inventory[1] = null;
                        inv.dirty = true;
                    }
                }
            }
        }
    }
}
