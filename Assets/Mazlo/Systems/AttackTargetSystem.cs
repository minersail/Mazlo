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
        private struct AttackTargetData
        {
            public ComponentArray<Transform> Transforms;
            public ComponentArray<AttackTargetComponent> AttackTargets;
            public ComponentArray<AttackComponent> AttackComponents;
            public int Length;
        }

        [Inject]
        private AttackTargetData AttackTargetEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < AttackTargetEntities.Length; i++)
            {
                Transform trans = AttackTargetEntities.Transforms[i];
                AttackTargetComponent attackTarget = AttackTargetEntities.AttackTargets[i];
                AttackComponent attackComponent = AttackTargetEntities.AttackComponents[i];

                attackComponent.attackTriggered = Vector3.Distance(trans.position, attackTarget.targetTrans.position) < attackTarget.attackDistance;
            }
        }
    }
}
