using UnityEngine;

namespace Environment
{
    public class Lantern : Interactable
    {
        [SerializeField] private Light _light;

        public override void Interact()
        {
            _light.enabled = !_light.enabled;
        }
    }
}