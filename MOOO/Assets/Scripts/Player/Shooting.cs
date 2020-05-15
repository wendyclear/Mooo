using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float _bulletSpeed;

    public bool shoot;

    public Camera _camera;

    private int _far = 1;
    public float _up = 0;
    private int _force = 80;


    private void Start()
    {
        _bulletSpeed = 100;
    }
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(bullet) as GameObject;
            projectile.transform.position = _camera.transform.position * _far ;
            projectile.GetComponent<Rigidbody>().velocity = _camera.transform.forward * _force;

        }
    }
}

/*

projectile.transform.position = transform.position +Camera.main.transform.forward*2;

rb.velocity= Camera.main.transform.forward* 40;*/
