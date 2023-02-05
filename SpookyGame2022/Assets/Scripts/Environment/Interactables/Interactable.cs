using UnityEngine;

namespace Environment
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Interactable")]
        [SerializeField] private Collider _collider;
        [SerializeField] private float _extraInteractionDistance = 0.0f;
        private float _distanceAway = 0.0f;
        public Collider Collider => _collider;
        public float ExtraInteractionDistance => _extraInteractionDistance;
        public float DistanceAway => _distanceAway;

        public abstract void Interact();

        public Interactable GetCollision(Vector3 position, Vector3 direction, ref RaycastHit[] raycastHits,  float distance)
        {
            int results = Physics.RaycastNonAlloc(position, direction, raycastHits, distance);

            if (results == 0)
            {
                return null;
            }

            for(int i = 0; i < results; ++i)
            {
                if (raycastHits[i].collider == _collider)
                {
                    _distanceAway = raycastHits[i].distance;
                    return this;
                }
            }

            return null;
        }
    }   
}