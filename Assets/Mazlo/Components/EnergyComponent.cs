using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mazlo.Components
{
    public class EnergyComponent : MonoBehaviour
    {
        public float maxEnergy;
        public float regenRate;
        [HideInInspector] public float energy;
        [HideInInspector] public bool regenEnabled;

        public void Start()
        {
            energy = maxEnergy;
        }
    }
}
