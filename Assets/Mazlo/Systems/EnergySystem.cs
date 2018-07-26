using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

namespace Mazlo.Systems
{
    public class EnergySystem : ComponentSystem
    {
        private struct EnergyData
        {
            public ComponentArray<EnergyComponent> EnergyComponents;
            public int Length;
        }

        [Inject]
        private EnergyData EnergyEntities;

        protected override void OnUpdate()
        {
            for (int i = 0; i < EnergyEntities.Length; i++)
            {
                EnergyComponent energy = EnergyEntities.EnergyComponents[i];

                energy.energy = Mathf.Clamp(energy.energy, 0, energy.maxEnergy);

                if (energy.regenEnabled)
                {
                    energy.energy += energy.regenRate * Time.deltaTime;
                }
            }
        }
    }
}
