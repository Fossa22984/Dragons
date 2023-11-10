using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [field: SerializeField] public bool ButtonPressed { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonPressed = false;
    }
}
