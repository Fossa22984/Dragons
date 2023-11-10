using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IJoystick
{
    [SerializeField] private GameObject _joystick;

    [SerializeField] private GameObject _thumble;

    [SerializeField] private float _radius;

    private Vector3 _startPosition;

    public Vector2 PowerOfDirection { get; private set; }
    public bool IsActive { get; private set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _joystick.transform.position = eventData.position;
        _thumble.transform.position = _joystick.transform.position;

        _startPosition = _joystick.transform.position;
        IsActive = true;

        SetJoystickVisible(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var distance = Vector3.Distance(_startPosition, eventData.position);
        var vectorDirection = (Vector3)eventData.position - _startPosition;
        distance = Mathf.Clamp(distance, 0, _radius);
        var direction = vectorDirection.normalized;

        if (distance <= _radius)
        {
            _thumble.transform.position = _startPosition + direction * distance;
        }
        PowerOfDirection = (Vector2)direction * distance / _radius;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetJoystickVisible(false);
        IsActive = false;
    }

    void Start()
    {
        SetJoystickVisible(false);
    }

    private void SetJoystickVisible(bool visible)
    {
        _joystick.SetActive(visible);
    }
}

public interface IJoystick
{
    Vector2 PowerOfDirection { get; }
    bool IsActive { get; }
}
