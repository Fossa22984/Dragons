using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "FPS/Counterpicks Data", fileName = "CounterpicksData", order = 0)]
public class CounterpicksData : ScriptableObject
{
    public IReadOnlyCollection<Counterpicks> Counterpicks => _counterpicks;
    [SerializeField] private List<Counterpicks> _counterpicks = new List<Counterpicks>();

    public List<ElementType> GetListGoodAgainst(ElementType pick)
    {
        var result = _counterpicks.Find(data => data.Pick == pick);
        return result.GoodAgainst;
    }

    public List<ElementType> GetListBadAgainst(ElementType pick)
    {
        var result = _counterpicks.Find(data => data.Pick == pick);
        return result.BadAgainst;
    }
}

[System.Serializable]
public class Counterpicks
{
    [field: SerializeField] public ElementType Pick { get; private set; }
    [field: SerializeField] public List<ElementType> GoodAgainst { get; private set; }
    [field: SerializeField] public List<ElementType> BadAgainst { get; private set; }
}