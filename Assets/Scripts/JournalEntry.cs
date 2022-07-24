
using StarterAssets;
using UnityEngine;

public class JournalEntry: MonoBehaviour, ITriggerable
{
    public string entryName;
    public string journalEntry;
    public int triggerCount;

    public void Trigger(GameObject other)
    {
        if (triggerCount < 1) return;
        
        if (other.name == "Player")
        {
            WorldState.World.AddToJournal(this);
            triggerCount--;
        }
    }

    public int GetTriggerCount()
    {
        return triggerCount;
    }
}
