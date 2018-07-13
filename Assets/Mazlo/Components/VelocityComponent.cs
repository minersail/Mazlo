using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mazlo.Components
{
    public class VelocityComponent : MonoBehaviour
    {
        public float maxSpeed;

        [HideInInspector] public float velocityX;
        [HideInInspector] public float velocityY;
    }
}
