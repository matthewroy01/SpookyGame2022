using UnityEngine;

namespace Environment
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Interactable")]
        [SerializeField] private Collider _collider;
        public Collider Collider => _collider;

        public abstract void Interact();

        public bool GetCollision(Vector3 position, Vector3 direction, ref RaycastHit[] raycastHits,  float distance)
        {
            int results = Physics.RaycastNonAlloc(position, direction, raycastHits, distance);

            if (results == 0)
            {
                return false;
            }

            foreach (RaycastHit raycastHit in raycastHits)
            {
                if (raycastHit.collider == _collider)
                {
                    return true;
                }
            }

            return false;
        }
    }   
}