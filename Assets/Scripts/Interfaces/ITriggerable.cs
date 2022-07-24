using UnityEngine;

namespace StarterAssets
{
    public interface ITriggerable
    {
        public void Trigger(GameObject other);
        public int GetTriggerCount();
    }
}