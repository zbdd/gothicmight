using UnityEngine;

namespace StarterAssets
{
    public interface IInteractable
    {
        public void OnInteract(GameObject other);
    }
}