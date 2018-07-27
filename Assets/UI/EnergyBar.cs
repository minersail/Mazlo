using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mazlo.Components;

public class EnergyBar : MonoBehaviour
{
    public Image energyBar;
    public Image flash;
    public EnergyComponent energyComponent;

    private float depleteTime;
    
	void Update ()
    {
        energyBar.fillAmount = energyComponent.energy / energyComponent.maxEnergy;

        if (energyComponent.energy <= 0)
        {
            depleteTime += Time.deltaTime;

            Color newColor = flash.color;
            newColor.a = (newColor.a + (Mathf.Sin(depleteTime * 2) + 1) / 2) / 2;
            flash.color = newColor;
        }
        else
        {
            depleteTime = 0;

            Color newColor = flash.color;
            newColor.a = Mathf.Lerp(newColor.a, 0, Time.deltaTime * 2);
            flash.color = newColor;
        }
	}
}
