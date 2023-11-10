using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;

public class Fly : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;

    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _groundCheck;

    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _stamina = 100f;
    [SerializeField] private float _jumpHeight;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundDistance = 0.4f;

    [SerializeField] private Button _flyButton;

    private Vector3 _velocity;
    private bool _isGrounded;
    private float _currentStamina;

    public void ChangeJumpHeight(float jumpHeight)
    {
        _jumpHeight = jumpHeight;
    }

    private void Start()
    {
        _flyButton.onClick.AddListener(OnFlyButtonClickHandler);

        _currentStamina = _stamina;
        _playerManager.ChangeStamina(_stamina, _currentStamina);
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = ConstVar.Deviation;
            if (_currentStamina < _stamina)
            {
                _currentStamina += 0.5f;
                _playerManager.ChangeStamina(_stamina, _currentStamina);
            }
        }

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnFlyButtonClickHandler()
    {
        if (_currentStamina > 0)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * ConstVar.Deviation * _gravity);
            _currentStamina -= 10f;
            _playerManager.ChangeStamina(_stamina, _currentStamina);
        }
    }
}