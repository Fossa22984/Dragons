//using DG.Tweening;
using Assets.Scripts.Level;
using Assets.Scripts.Level.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelController : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> AlliedDragons { get; private set; }

    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private EndLevel _endLevel;

    [SerializeField] private List<GameObject> _enemyDragons;
    [SerializeField] private int _countEnemyDragons;

    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private DragonsData _dragonsDb;

    [SerializeField] private NavMeshAgent _agent;

    private int _respawn;

    public void ChangeDragon(int index)
    {
        StartCoroutine(Move(_playerManager.Player.transform, _spawnPoints[_respawn].transform, ConstVar.Duration));
        StartCoroutine(RotateQuaternion(_playerManager.Player.transform, _spawnPoints[_respawn].transform, ConstVar.Duration));


        _playerManager.ChangeDragon(AlliedDragons[index], AlliedDragons[index].GetComponent<DragonInfo>(), _levelUI.GetDragonItem(index));
    }


    public void DeathDragon(GameObject dragon)
    {
        for (int i = 0; i < AlliedDragons.Count; i++)
        {
            if (!AlliedDragons[i].GetComponent<DragonInfo>().IsDead)
            {
                ChangeDragon(i);
                return;
            }
        }
        _endLevel.Finish(EndType.Lost);
    }

    public void DeathEnemyDragon(GameObject dragon)
    {
        for (int i = 0; i < _enemyDragons.Count; i++)
        {
            if (!_enemyDragons[i].GetComponent<DragonInfo>().IsDead)
                return;
        }
        _endLevel.Finish(EndType.Win);
    }



    private void Awake()
    {
        _respawn = Random.Range(0, _spawnPoints.Count);
        _spawnPoints[_respawn].IsEmpty = false;

        FillListAlliedDragons();
        SelectEmenyDragons();

        _levelUI.InitListInUi(AlliedDragons);
    }

    void Start()
    {
        AudioManager.Instance.PlayMusic(MusicType.Game);
        ChangeDragon(0);
    }

    private void FillListAlliedDragons()
    {
        for (int i = 0; i < GameController.Instance.CountDragon; i++)
        {
            var dragonData = GameController.Instance.GetDragonByIndex(i);
            var dragon = LoadPrefab(dragonData.DragonPath, _playerManager.Player.transform);
            dragon.GetComponent<DragonInfo>().FillInfo(dragonData);
            dragon.GetComponent<CharacterController>().enabled = false;
            dragon.SetActive(false);
            AlliedDragons.Add(dragon);
        }
    }

    private void SelectEmenyDragons()
    {
        for (int i = 0; i < _countEnemyDragons; i++)
        {
            var dragonData = _dragonsDb.GetDragonByIndex(Random.Range(0, _dragonsDb.Dragons.Count));
            for (int j = 0; j < _spawnPoints.Count; j++)
            {
                if (_spawnPoints[j].IsEmpty)
                {
                    var dragon = _spawnPoints[j].FillSpawnPoint(dragonData.DragonPath);
                    dragon = CreateEmenyDragons(dragonData, dragon);

                    _enemyDragons.Add(dragon);
                    break;
                }
            }
        }
    }
    private GameObject CreateEmenyDragons(Dragon dragonData, GameObject dragon)
    {

        var navMeshAgent = dragon.AddComponent<NavMeshAgent>();
        navMeshAgent.speed = ConstVar.Speed;
        navMeshAgent.radius = ConstVar.Radius;
        navMeshAgent.height = ConstVar.Heigh;

        var randomMovement = dragon.AddComponent<RandomMovement>();
        randomMovement.Fill(navMeshAgent, ConstVar.Range, navMeshAgent.transform);

        var dragonInfo = dragon.GetComponent<DragonInfo>();
        dragonInfo.FillInfo(dragonData);

        dragon.GetComponent<Assets.Scripts.Level.Target>().Fill(this, dragon, dragonInfo);

        return dragon;
    }

    private GameObject LoadPrefab(string path, Transform parant)
    {
        var prefab = Resources.Load(path);
        var dragon = (GameObject)Instantiate(prefab, parant);
        return dragon;
    }

    private IEnumerator Move(Transform target, Transform spawnPoints, float duration)
    {
        var timeCounter = 0f;
        while (timeCounter < duration)
        {
            var normalizedTime = timeCounter / duration;
            target.position = Vector3.Lerp(target.position, spawnPoints.position, normalizedTime);
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator RotateQuaternion(Transform target, Transform spawnPoints, float duration)
    {
        var timeCounter = 0f;
        while (timeCounter < duration)
        {
            var normalizedTime = timeCounter / duration;
            target.rotation = Quaternion.Lerp(target.rotation, spawnPoints.rotation, normalizedTime);
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }

}