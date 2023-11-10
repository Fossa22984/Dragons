using UnityEngine;

namespace Assets.Scripts
{
    public class VoidSpase : MonoBehaviour
    {
        [SerializeField] private float _damage;

        private void OnTriggerEnter(Collider other)
        {
            var target = other.transform.GetComponent<HealthPointController>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
        }
    }
}