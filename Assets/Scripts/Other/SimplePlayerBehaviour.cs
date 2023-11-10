using UnityEngine;
using UnityEngine.AI;

public class SimplePlayerBehaviour : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                SetTargetPoint(hit.point);
            }
        }
    }

    private void SetTargetPoint(Vector3 point)
    {
        _agent.SetDestination(point);
    }
}
