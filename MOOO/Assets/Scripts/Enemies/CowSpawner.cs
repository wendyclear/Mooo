using System.Collections.Generic;
using UnityEngine;

public class CowSpawner : MonoBehaviour
{
    // The GameObject to instantiate.
    [SerializeField]
    private GameObject _enemyToSpawn;

    // An instance of the ScriptableObject defined above.
    [SerializeField]
    private ScriptableCow _scriptableCow;

    [SerializeField]
    private List<GameObject> _cows;

    // This will be appended to the name of the created entities and increment when each is created.
    private int instanceNumber = 1;

    void Start()
    {
        SpawnCows();
    }

    private void Update()
    {
        RespawnCows();
    }

    private void SpawnCows()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < _scriptableCow._numberOfCows; i++)
        {
            GameObject currentEntity = Instantiate(_enemyToSpawn, _scriptableCow._spawnPoints[currentSpawnPointIndex], Quaternion.identity);
            currentEntity.name = _scriptableCow._prefabName + instanceNumber;
            currentEntity.transform.parent = GetComponent<Transform>();
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % _scriptableCow._spawnPoints.Length;
            _cows.Add(currentEntity);
            instanceNumber++;
        }
    }

    private void RespawnCows()
    {
        for (int i = 0; i < _scriptableCow._numberOfCows; i++)
        {
            if (!_cows[i].activeInHierarchy)
            {
                if (_cows[i].GetComponent<Cow>().GetCurrentWaitTime() >= _cows[i].GetComponent<Cow>().GetMaxWaitTime())
                {
                    _cows[i].GetComponent<Transform>().position = _scriptableCow._spawnPoints[i];
                    _cows[i].SetActive(true);

                }
                else
                {
                    _cows[i].GetComponent<Cow>().AddCurrentWaitTime(Time.deltaTime);
                }
            }
        }
    }
}