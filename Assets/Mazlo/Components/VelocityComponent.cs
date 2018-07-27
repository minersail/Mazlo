using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mazlo.Components
{
    public class VelocityComponent : MonoBehaviour
    {
        public float maxSpeed;

        [HideInInspector] public float inputX;
        [HideInInspector] public float inputY;

        [HideInInspector] public float movementMultiplier;

        public void Start()
        {
            movementMultiplier = 1;
        }
    }
}
