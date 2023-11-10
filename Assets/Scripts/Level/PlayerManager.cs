using UnityEngine;

namespace Assets.Scripts.Level
{
    public class PlayerManager : MonoBehaviour
    {
        [field: SerializeField] public GameObject Player { get; private set; }

        [SerializeField] private GameObject _dragon;
        [SerializeField] private DragonInfo _dragonInfo;

        [SerializeField] private Fly _flyController;
        [SerializeField] private MovementController _movementController;
        [SerializeField] private HealthPointController _healthPointController;

        [SerializeField] private HitscanAttack _hitscanAttack;

        [SerializeField] private LevelController _levelController;

        [SerializeField] private UIDragonInfo _uiDragonInfo;
        [SerializeField] private DragonItem _dragonItemView;

        public void ChangeDragon(GameObject dragon, DragonInfo dragonInfo, DragonItem dragonItemView)
        {
            if (_dragon != null)
                _dragon.SetActive(false);

            _dragon = dragon;
            _dragonInfo = dragonInfo;
            _dragonItemView = dragonItemView;

            _flyController.ChangeJumpHeight(_dragonInfo.FlappingForce);
            _movementController.ChangeSpeed(_dragonInfo.MoveSpeed);
            _healthPointController.ChangeHealthPoint(_dragonInfo.HealthPoint, dragonItemView.CurrentHealthPoint, _dragonInfo.Type);

            _hitscanAttack.ChangeAttack(dragonInfo);

            _dragon.SetActive(true);
        }

        public void ChangeStamina(float maxStamina, float currentStamina)
        {
            _uiDragonInfo.ChangeStamina(maxStamina, currentStamina);
        }

        public void ChangeHp(float maxHp, float currentHp)
        {
            _uiDragonInfo.ChangeHp(maxHp, currentHp);

            if (_dragonItemView != null)
                _dragonItemView.ChangeHp(currentHp);
        }

        public void DeathDragon()
        {
            _dragon.SetActive(false);
            _dragonInfo.IsDead = true;
            _levelController.DeathDragon(_dragon);
        }
    }
}