using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private BulletBehaviour _bulletPrefab;
    [SerializeField] private GameObject _ExplosionPrefab;

    [SerializeField] private Transform _bulletStartPoint;

    [SerializeField] private List<InitializePoolData> _initializePool = new List<InitializePoolData>();

    [SerializeField] private Transform _parentForPool;

    [SerializeField] private Button _hitscanAttackButton;
    [SerializeField] private Button _projectileAttackButton;

    // Start is called before the first frame update
    void Start()
    {
        PreparePool();
        _hitscanAttackButton.onClick.AddListener(HitscanAttack);
        _projectileAttackButton.onClick.AddListener(ProjectileAttack);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void HitscanAttack()
    {

        PrepareBullet(_ExplosionPrefab);
    }

    private void ProjectileAttack()
    {

        PrepareBullet(_bulletPrefab.gameObject);
    }


    private void PrepareBullet(GameObject bulletPrefab)
    {
        var bulletFromPool = PoolManager.GetObject(bulletPrefab.gameObject);
        var bulletBehaviour = bulletFromPool.GetComponent<BulletBehaviour>();
        bulletBehaviour.SetMotionData(_bulletStartPoint.position, _bulletStartPoint.rotation);
        bulletBehaviour.StartMotion();
    }

    private void PreparePool()
    {
        PoolManager.SetParentForPoolObjects(_parentForPool);
        foreach (var poolData in _initializePool)
        {
            PoolManager.InitializePool(poolData.Prefab, poolData.Count);
        }
    }
}


[System.Serializable]
public class InitializePoolData
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
}
