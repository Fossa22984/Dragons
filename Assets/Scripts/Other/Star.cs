using Assets.Scripts.Level.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private EndLevel _endLevel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _endLevel.Finish(EndType.RanAway);
            }
        }
    }
}