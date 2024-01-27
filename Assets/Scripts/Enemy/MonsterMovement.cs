using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    [Header("NavMeshAgent Settings")]
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _monsterSpeed = 3.5f;

    [Header("Detection Settings")]
    [SerializeField][Range(0, 360)] private float _defaultViewAngle = 90f; 
    [SerializeField] private float _viewDistance = 15f; 
    [SerializeField] private Transform _target;

    private float _viewAngle;

    private void Start()
    {
        _viewAngle = _defaultViewAngle;
        _navMeshAgent.speed = _monsterSpeed;
        SetNewPatrolPoint();
    }

    private void Update()
    {
        DrawView();
        float distanceToPlayer = Vector3.Distance(_target.transform.position, _navMeshAgent.transform.position);
        if (distanceToPlayer <= _viewDistance)
        {
            _viewAngle = 360;
        }
        else
        {
            _viewAngle = _defaultViewAngle;
        }
        if (IsInView()) 
        {
            if (distanceToPlayer >= 3f) 
            {
                MoveToTarget(); 
            }
            else
            {
                _navMeshAgent.isStopped = true; 
            }
        }
        else 
        {
            if (_navMeshAgent.remainingDistance <= 0.25f)
            {
                StartCoroutine(WaitAndGo());
            }
        }
    }

    private void SetNewPatrolPoint()
    {
        Vector3 newPoint = _patrolPoints[Random.Range(0, _patrolPoints.Length)].position;
        _navMeshAgent.SetDestination(newPoint);
    }

    private bool IsInView()
    {
        float currentAngle = Vector3.Angle(transform.forward, _target.position - transform.position);
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + new Vector3(0, 1.5f, 0);
        Vector3 raycastDirection = _target.position - transform.position;
        Debug.DrawRay(raycastOrigin, raycastDirection, Color.red);
        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, _viewDistance))
        {
            if (currentAngle < _viewAngle / 2f && Vector3.Distance(transform.position, _target.position) <= _viewDistance && hit.collider.CompareTag("Player"))
            {
                return true; 
            }
        }
        return false; 
    }

    private void DrawView()
    {
        Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, _viewAngle / 2f, 0)) * (transform.forward * _viewAngle);
        Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, _viewAngle / 2f, 0)) * (transform.forward * _viewAngle);
        Debug.DrawLine(transform.position, left, Color.yellow);
        Debug.DrawLine(transform.position, right, Color.yellow);
    }

    private void MoveToTarget()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = 3.5f;
        _navMeshAgent.SetDestination(_target.position);
    }

    private IEnumerator WaitAndGo()
    {
        _navMeshAgent.isStopped = true;
        SetNewPatrolPoint();
        yield return new WaitForSeconds(3f);
        _navMeshAgent.isStopped = false;
    }
}
