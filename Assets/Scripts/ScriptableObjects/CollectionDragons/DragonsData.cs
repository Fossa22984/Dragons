using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/Dragons Data", fileName = "DragonsData", order = 0)]
public class DragonsData : ScriptableObject
{
    public IReadOnlyCollection<Dragon> Dragons => _dragons;
    [SerializeField] private List<Dragon> _dragons = new List<Dragon>();

    public Dragon GetDragonByName(string name)
    {
        var result = _dragons.Find(data => data.Name == name);
        return result;
    }

    public Dragon GetDragonByIndex(int index)
    {
        var result = _dragons[index];
        return result;
    }
}

[System.Serializable]
public class Dragon
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public float HealthPoint { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float FlappingForce { get; private set; }
    // [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public ElementType Type { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    [field: SerializeField] public string DragonPreviewPath { get; private set; }
    [field: SerializeField] public string DragonPath { get; private set; }
}