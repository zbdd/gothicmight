using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public List<ITriggerable> itemsToTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        if (itemsToTrigger == null) itemsToTrigger = new List<ITriggerable>(0);
        if (!GetComponent<JournalEntry>()) return;
        itemsToTrigger.Add(GetComponent<JournalEntry>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Location"))
        {
            Details deets = other.GetComponent<Details>();
            if (deets)
            {
                WorldState.World.AddToAreasVisited(deets.name);
            }
        }
        
        if (itemsToTrigger.Count <= 0) return;
        
        foreach (ITriggerable triggerable in itemsToTrigger)
        {
            triggerable.Trigger(other.gameObject);
            
        }
    }
}
