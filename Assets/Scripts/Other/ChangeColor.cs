using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ChangeColor : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _particleSystems;
        [SerializeField] private float _number;

        public void ChangeParticleSystemColor(Color color)
        {
            for (int i = 0; i < _particleSystems.Count; i++)
            {
                var main = _particleSystems[i].main;
                main.startColor = new Color(color.r - i * _number, color.g - i * _number, color.b - i * _number);
            }
        }
    }
}