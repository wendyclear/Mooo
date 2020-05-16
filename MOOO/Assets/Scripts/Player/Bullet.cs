using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

}
