using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mazlo.Components;

public class EnergyBar : MonoBehaviour
{
    public Image energyBar;
    public EnergyComponent energyComponent;
    
	void Update ()
    {
        energyBar.fillAmount = energyComponent.energy / energyComponent.maxEnergy;
	}
}
