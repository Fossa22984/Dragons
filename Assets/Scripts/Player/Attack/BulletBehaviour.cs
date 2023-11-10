using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _autoDestroyTime = 4f;

    private bool _isInMotion = false;

    // Start is called before the first frame update

    public void SetMotionData(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void StartMotion()
    {
        _isInMotion = true;
        gameObject.SetActive(true);
        Invoke(nameof(AutoDestroy), _autoDestroyTime);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isInMotion == false)
            return;

        MoveForward();
    }

    private void MoveForward()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void AutoDestroy()
    {
        //Destroy(gameObject);
        _isInMotion = false;
        PoolManager.PutObject(gameObject);
    }
}
