using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _map1;
    [SerializeField] private string _map2;

    [SerializeField] private GameObject _button;
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    [SerializeField] private GameObject _selectModeView;

    public void SeleckDragonsOnClick()
    {
        if (GameController.Instance.CountDragon == 0)
            _button.SetActive(true);
        else _button.SetActive(false);

        ClearSpawnPoints();
        FillSpawnPoints();
    }
    public void PlayOnClick()
    {
        if (GameController.Instance.CountDragon > 0)
            _selectModeView.SetActive(true);
    }

    public void WildDragonOnClick() => SceneManager.LoadScene(_map1);
    public void BossFightOnClick() => SceneManager.LoadScene(_map2);

    private void Start()
    {
        AudioManager.Instance.PlayMusic(MusicType.MainMenu);
    }

    private void ClearSpawnPoints()
    {
        foreach (var item in _spawnPoints)
            item.EmptySpawnPoint();
    }
    private void FillSpawnPoints()
    {
        for (int i = 0; i < GameController.Instance.CountDragon; i++)
        {
            var path = GameController.Instance.GetDragonByIndex(i).DragonPreviewPath;
            _spawnPoints[i].FillSpawnPoint(path);
        }
    }
}