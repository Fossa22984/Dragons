using UnityEngine;

public class CollectionDragons : MonoBehaviour
{
    [SerializeField] private DragonsData _dragonsDb;
    [SerializeField] private DragonItemView _dragonItemViewPrefab;
    [SerializeField] private Transform _parentForDragonItem;

    void Start()
    {
        InitStore();
    }

    private void InitStore()
    {
        foreach (var dragon in _dragonsDb.Dragons)
            CreateCollectionDragoItem(dragon);
    }

    private void CreateCollectionDragoItem(Dragon dragon)
    {
        var dragonItemView = Instantiate(_dragonItemViewPrefab, _parentForDragonItem);
        dragonItemView.SetDragonData(dragon.Name, dragon.Type, dragon.Icon, dragon.Color);

        dragonItemView.SetSelectToggleClickCallback(() => OnSelectDragonToggleHandler(dragon.Name, dragonItemView.IsOn));
    }

    private void OnSelectDragonToggleHandler(string name, bool isOn)
    {
        if (isOn) { OnSelect(name); }
        else { OnDeselect(name); }
    }

    private void OnSelect(string name)
    {
        if (GameController.Instance.CountDragon < GameController.Instance.MaxCountDragon)
        {
            var dragon = _dragonsDb.GetDragonByName(name);
            GameController.Instance.AddDragon(dragon);
        }
    }

    private void OnDeselect(string name)
    {
        GameController.Instance.RemoveDragonByName(name);
    }
}