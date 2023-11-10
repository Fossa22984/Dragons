using Assets.Scripts.Level;
using UnityEngine;

public class HealthPointController : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;

    [SerializeField] private float _healthPoint;
    private float _currentHealtha;

    private ElementType _elementType;

    public void ChangeHealthPoint(float healthPoint, float currentHealtha, ElementType elementType)
    {
        _elementType = elementType;
        _currentHealtha = currentHealtha;
        _healthPoint = healthPoint;

        _playerManager.ChangeHp(_healthPoint, _currentHealtha);
    }

    void Start()
    {
        _currentHealtha = _healthPoint;
        _playerManager.ChangeHp(_healthPoint, _currentHealtha);
    }

    public void TakeDamage(float amount)
    {
        AudioManager.Instance.PlaySfx(_elementType, SfxType.TakeDamag);
        _currentHealtha -= amount;

        _playerManager.ChangeHp(_healthPoint, _currentHealtha);

        if (_currentHealtha <= 0f)
            _playerManager.DeathDragon();
    }
}
