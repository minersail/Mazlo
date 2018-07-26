using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mazlo.Components
{
    public class EnergyComponent : MonoBehaviour
    {
        public float maxEnergy;
        [HideInInspector] public float energy;
    }
}
