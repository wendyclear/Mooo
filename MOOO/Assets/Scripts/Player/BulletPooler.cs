using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public static BulletPooler pooler;

    public GameObject _bullet;
    public int _numOfBullets;

    public List<GameObject> _bullets;

    private void Awake()
    {
        pooler = this;
    }

    private void Start()
    {
        _bullets = new List<GameObject>();
        for (int i = 0; i< _numOfBullets; i++)
        {
            GameObject bullet = (GameObject)Instantiate(_bullet);
            bullet.transform.parent = GetComponent<Transform>();
            bullet.SetActive(false);
            _bullets.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < _numOfBullets; i++)
        {
            if (!_bullets[i].activeInHierarchy)
            {
                return _bullets[i];
            }
        }
        return null;
    }
}
