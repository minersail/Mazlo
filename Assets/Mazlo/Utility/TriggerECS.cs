using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Mazlo.Components;

/// <summary>
/// This utility class will be placed on any gameobject with a trigger, 
/// and will update its reference to a TriggerComponent with any triggers
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))] // Necessary for triggers on non-rigidbody objects; make sure to set as kinematic
public class TriggerECS : MonoBehaviour
{
    public TriggerComponent trackedTC;
    Collider trigger;

	void Start ()
    {
        trigger = GetComponent<Collider>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<GameObjectEntity>() != null)
        {
            trackedTC.triggers.Add(new TriggerComponent.TriggerData(other, other.transform.GetComponent<GameObjectEntity>().Entity));
        }
        else
        {
            trackedTC.triggers.Add(new TriggerComponent.TriggerData(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < trackedTC.triggers.Count; i++)
        {
            TriggerComponent.TriggerData data = trackedTC.triggers[i];

            if (data.trigger.Equals(other))
            {
                trackedTC.triggers.Remove(data);
            }
        }
    }
}
