using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cow : MonoBehaviour
{
    [SerializeField]
    private float _hp;
    [SerializeField]
    private float _maxHp;

    [SerializeField]
    private Renderer _healthBar;

    [SerializeField]
    private GameObject _bucketPrefab;


    private float _rStart = 132f;
    private float _gStart = 255f;
    private float _bStart = 0f;
    private float _rEnd   = 255f;
    private float _gEnd   = 0f;
    private float _bEnd   = 0f;

    [SerializeField]
    private float _waitTime;
    [SerializeField]
    private float _currentWaitTime;

    [SerializeField]
    private float _standTime;
    [SerializeField]
    private float _currentStandTime;

    private float _alpha;
    private Vector3 _walkTo;

    private NavMeshAgent _navMeshAgent;
    private Transform _position;

    private void Start()
    {
        _maxHp = 100;
        _alpha = 0.5f;
        _waitTime = 3;
        _standTime = 10;
        Initialize();
        _position = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void Update()
    {
        CheckMoooving();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("bullet"))
        {
            
            _hp -= collision.gameObject.GetComponent<Bullet>().Damage();
            Color c = new Color((_rStart + (_rEnd - _rStart) * ( 1 - (_hp / _maxHp)))/255, (_gStart + (_gEnd - _gStart) * (1- (_hp / _maxHp)))/255, (_bStart + (_bEnd - _bStart) * (1- (_hp / _maxHp)))/255, _alpha);
            _healthBar.material.SetColor("_Color", c);
            if (_hp <= 0)
            {
                gameObject.SetActive(false);
                SpawnBucket();
            }
        }
    }

    public float GetCurrentWaitTime()
    {
        return _currentWaitTime;
    }
    public void AddCurrentWaitTime(float time)
    {
        _currentWaitTime += time;
    }     
    public float GetMaxWaitTime()
    {
        return _waitTime;
    }    

    private void Initialize()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentWaitTime = 0;
        _healthBar.material.color = new Color(_rStart / 255, _gStart / 255, _bStart / 255, 1);
        _hp = _maxHp;
        _currentStandTime = 0;
        GetRandomLocation(60);
        _navMeshAgent.SetDestination(_walkTo);
    }


    public void GetRandomLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        _walkTo = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            _walkTo = hit.position;
        }
    }

    private void CheckMoooving()
    {
        if (!_navMeshAgent.pathPending && (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)))
        {
            GetRandomLocation(Random.Range(50,200));
            _navMeshAgent.SetDestination(_walkTo);
        }
    }

    private void SpawnBucket()
    {
        GameObject bucket = GameObject.Instantiate(_bucketPrefab);
        Vector3 _pos = _position.position;
        bucket.transform.position = new Vector3(_pos.x, bucket.transform.position.y, _pos.z);
    }

}
