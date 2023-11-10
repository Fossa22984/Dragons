using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public readonly int MaxCountDragon = 5;

    public IReadOnlyCollection<Dragon> Dragons => _dragons;
    public int CountDragon => _countDragon;

    [SerializeField] private List<Dragon> _dragons = new List<Dragon>();
    private int _countDragon;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

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

    public void RemoveDragonByName(string name)
    {
        var result = _dragons.Find(data => data.Name == name);
        if (result != null)
        {
            _dragons.Remove(result);
            _countDragon--;
        }
    }

    public void AddDragon(Dragon item)
    {
        _dragons.Add(item);
        _countDragon++;
    }
}