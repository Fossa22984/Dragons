using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _rotationSpeed = 100f;

    [SerializeField] private JoystickDetector _rotationDetector;
    private IJoystick _rotationJoystick;

    private float _xRotation = 0f;

    void Start()
    {
        _rotationJoystick = _rotationDetector;
    }

    void Update()
    {
        if (_rotationJoystick.IsActive)
            Rotation();
    }
    private void Rotation()
    {
        float mouseX = _rotationJoystick.PowerOfDirection.x * _rotationSpeed * Time.deltaTime;
        float mouseY = _rotationJoystick.PowerOfDirection.y * _rotationSpeed * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseX);
    }
}
