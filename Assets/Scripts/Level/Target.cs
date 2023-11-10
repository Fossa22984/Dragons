using UnityEngine;

namespace Assets.Scripts.Level
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private CounterpicksData _counterpicksData;

        [SerializeField] private GameObject _dragon;
        [SerializeField] private DragonInfo _dragonInfo;

        [SerializeField] private float _amount = 10f;

        private float _currentHealtha;

        public void Fill(LevelController levelController, GameObject dragon, DragonInfo dragonInfo)
        {
            _levelController = levelController;
            _dragon = dragon;
            _dragonInfo = dragonInfo;
            _currentHealtha = dragonInfo.HealthPoint;
        }

        public void TakeDamage(ElementType enemyElementType)
        {
            //AudioManager.Instance.PlaySfx(_dragonInfo.Type, SfxType.TakeDamag);

            var checkCounterpick = CheckCounterpick(enemyElementType);
            _currentHealtha -= GetDamage(checkCounterpick);

            if (_currentHealtha <= 0f)
                DeathDragon();
        }

        private void DeathDragon()
        {
            _dragon.SetActive(false);
            _dragonInfo.IsDead = true;
            _levelController.DeathEnemyDragon(_dragon);
        }

        private float CheckCounterpick(ElementType enemyElementType)
        {
            var goodAgainst = _counterpicksData.GetListGoodAgainst(_dragonInfo.Type);
            for (int i = 0; i < goodAgainst.Count; i++)
            {
                if (goodAgainst[i] == enemyElementType)
                    return 1;
            }

            var badAgainst = _counterpicksData.GetListBadAgainst(_dragonInfo.Type);
            for (int i = 0; i < badAgainst.Count; i++)
            {
                if (badAgainst[i] == enemyElementType)
                    return -1;
            }

            return 0;
        }

        private float GetDamage(float checkCounterpick)
        {
            switch (checkCounterpick)
            {
                case -1: return _amount * ConstVar.DamageBoost;
                case 1: return _amount * ConstVar.DamageBlock;
                default: return _amount;
            }
        }
    }
}