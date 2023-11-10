using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed;

    [SerializeField] private JoystickDetector _movementDetector;
    private IJoystick _movementJoystick;

    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }

    void Start()
    {
        _movementJoystick = _movementDetector;
    }

    void Update()
    {
        if (_movementJoystick.IsActive)
            Move();
    }

    private void Move()
    {
        float x = _movementJoystick.PowerOfDirection.x;
        float z = _movementJoystick.PowerOfDirection.y;

        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * _speed * Time.deltaTime);
    }
}