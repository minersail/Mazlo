using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    [UpdateBefore(typeof(AttackSystem))]
    public class AttackTargetSystem : ComponentSystem
    {
        private struct AttackTargetEntity
        {
            public Transform trans;
            public AttackTargetComponent attackTarget;
            public AttackComponent attackComponent;
        }

        protected override void OnUpdate()
        {
            foreach (AttackTargetEntity entity in GetEntities<AttackTargetEntity>())
            {
                entity.attackComponent.attackTriggered = Vector3.Distance(entity.trans.position, entity.attackTarget.targetTrans.position) < entity.attackTarget.attackDistance;             
            }
        }
    }
}
