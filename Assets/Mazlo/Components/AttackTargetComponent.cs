using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mazlo.Components
{
    [RequireComponent(typeof(AttackComponent))]
    public class AttackTargetComponent : MonoBehaviour
    {
        public Transform targetTrans;
        public float attackDistance;
    }
}
