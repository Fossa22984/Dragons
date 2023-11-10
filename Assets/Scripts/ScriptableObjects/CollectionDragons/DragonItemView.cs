using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragonItemView : MonoBehaviour
{
    public bool IsOn;

    [SerializeField] private TMP_Text _titleText;

    [SerializeField] private Image _dragonIcon;
    [SerializeField] private List<Image> _frame;
    [SerializeField] private Image _bordersIcon;
    [SerializeField] private GameObject _toggle;

    [SerializeField] private Toggle _selectToggle;
    private System.Action _selectToggleCallback;

    private string _titleFormat;

    public void SetDragonData(string name, ElementType type, Sprite icon, Color color)
    {
        _titleText.text = string.Format(_titleFormat, name, type.ToString());
        _dragonIcon.sprite = icon;

        foreach (var item in _frame)
            item.color = new Color(color.r, color.g, color.b);

        _bordersIcon.color = new Color(color.r * 0.5f, color.g * 0.5f, color.b * 0.5f);
    }

    public void SetSelectToggleClickCallback(System.Action onSelectToggleCallback)
    {
        _selectToggleCallback = onSelectToggleCallback;
    }

    private void Awake()
    {
        _titleFormat = _titleText.text;

        _selectToggle.onValueChanged.AddListener(OnSelectToggleClickHandler);
    }

    private void OnSelectToggleClickHandler(bool arg)
    {
        IsOn = arg;
        if (arg)
        {
            if (GameController.Instance.CountDragon < GameController.Instance.MaxCountDragon)
            {
                _selectToggleCallback?.Invoke();
                _toggle.SetActive(arg);
            }
            else _selectToggle.isOn = false;
        }
        else
        {
            _selectToggleCallback?.Invoke();
            _toggle.SetActive(arg);
        }
    }
}