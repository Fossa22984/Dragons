using Assets.Scripts;
using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;

public class HitscanAttack : MonoBehaviour
{
    //[SerializeField] private float _damage = 10f;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _fireRate = 15f;

    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private GameObject _impactEffect;

    [SerializeField] private Camera _camera;
    [SerializeField] private Button _shootButton;

    private ElementType _elementType;
    private Color _color;
    private float _nextTimeToFore;
    private bool _buttonPressed;

    public void ChangeAttack(DragonInfo dragon)
    {
        _elementType = dragon.Type;
        _color = dragon.Color;

        var changeColor = _muzzleFlash.GetComponent<ChangeColor>();
        changeColor.ChangeParticleSystemColor(_color);
    }

    void Update()
    {
        _buttonPressed = _shootButton.transform.GetComponent<MyButton>().ButtonPressed;

        if (_buttonPressed && Time.time >= _nextTimeToFore)
        {
            _nextTimeToFore = Time.time + 1f / _fireRate;
            OnShootButtonClick();
        }
    }

    private void OnShootButtonClick()
    {
        AudioManager.Instance.PlaySfx(_elementType, SfxType.Attack);

        _muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _range))
        {
            // Debug.Log(hit.transform.name);
            var target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                AudioManager.Instance.PlaySfx(_elementType, SfxType.GiveDamage);
                target.TakeDamage(_elementType);
            }
            var impactEffect = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            var changeColor = impactEffect.GetComponent<ChangeColor>();
            changeColor.ChangeParticleSystemColor(_color);
            Destroy(impactEffect, 3f);
        }
    }
}
