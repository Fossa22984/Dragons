using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _dragon;
    [field: SerializeField] public bool IsEmpty { get; set; }

    public GameObject FillSpawnPoint(string path)
    {
        var prefab = Resources.Load(path);
        _dragon = (GameObject)Instantiate(prefab, _spawnPoint);
        IsEmpty = false;
        return _dragon;
    }

    public void EmptySpawnPoint()
    {
        if (_dragon != null)
        {
            Destroy(_dragon);
            IsEmpty = true;
        }
    }
}