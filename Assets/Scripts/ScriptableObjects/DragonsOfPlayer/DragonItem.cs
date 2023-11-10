using UnityEngine;
using UnityEngine.UI;


public class DragonItem : MonoBehaviour
{
    [field: SerializeField] public float CurrentHealthPoint { get; private set; }
    [SerializeField] private float _healthPoint { get; set; }

    [SerializeField] private string _dragonName;
    [SerializeField] private Image _dragonIcon;
    [SerializeField] private Image _sliderHP;

    [SerializeField] private Button _selectButton;
    private System.Action _clickButtonCallback;

    public void SetDragonData(string name, float healthPoint, Sprite icon, Color color)
    {
        _dragonName = name;
        _dragonIcon.sprite = icon;
        _sliderHP.color = new Color(color.r, color.g, color.b);

        CurrentHealthPoint = _healthPoint = healthPoint;
        ChangeHp(CurrentHealthPoint);
    }

    public void ChangeHp(float currentHp)
    {
        CurrentHealthPoint = currentHp;
        _sliderHP.fillAmount = currentHp / _healthPoint;
    }

    public void SetSelectDragonClickCallback(System.Action onClickButtonCallback)
    {
        _clickButtonCallback = onClickButtonCallback;
    }

    private void Awake()
    {
        _selectButton.onClick.AddListener(OnClickButtonHandler);
    }

    private void OnClickButtonHandler()
    {
        if (CurrentHealthPoint > 0)
        {
            _clickButtonCallback?.Invoke();
        }
    }
}
