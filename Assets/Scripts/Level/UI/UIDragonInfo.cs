using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Level
{
    public class UIDragonInfo : MonoBehaviour
    {
        [SerializeField] private Image _hp;
        [SerializeField] private Image _stamina;

        public void ChangeHp(float maxHp, float currentHp)
        {
            _hp.fillAmount = currentHp / maxHp;

        }

        public void ChangeStamina(float maxStamina, float currentStamina)
        {
            _stamina.fillAmount = currentStamina / maxStamina;
        }
    }
}