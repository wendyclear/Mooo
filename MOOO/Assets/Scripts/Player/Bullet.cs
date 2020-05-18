using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _damage;

    private void Start()
    {
        _damage = 25;
    }
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    public float Damage()
    {
        return _damage;
    }

}
