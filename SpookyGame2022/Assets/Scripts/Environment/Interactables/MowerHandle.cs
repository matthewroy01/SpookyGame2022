using System;
using UnityEngine;
using UnityEngine.Events;

namespace Environment.Interactables
{
    public class MowerHandle : Interactable
    {
        public Action OnInteract;
        
        public override void Interact()
        {
            OnInteract?.Invoke();
        }
    }
}