using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Physics.IgnoreCollision(GameObject.Find("FirstPersonPlayer").GetComponent<CapsuleCollider>(), GetComponent<SphereCollider>(), true);
        //Physics.IgnoreCollision(GameObject.Find("FirstPersonPlayer").GetComponent<CharacterController>(), GetComponent<SphereCollider>(), true);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name != "Ground") Debug.Log(collision);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Ground") Debug.Log(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name != "Ground") Debug.Log(collision);
    }
}
