using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    private float   _rotateX;
    private float   _rotateY;
    private float   _sensitivity;
    private float   _cameraRotation;
                    
    private float   _moveX;
    private float   _moveY;
    private float   _moveSpeed;

    private float   _gravity;
    private float   _jumpHeight;
    private Vector3 _velocity;
    private bool    _onGround;

    private int _maxSightDistance;

    public GameObject Camera;
    public GameObject CanvasManager;

    void Start()
    {
        _sensitivity      = 4f;
        _cameraRotation   = 0f;
        _moveSpeed        = 30f;
        _gravity          = 9.81f*6;
        _jumpHeight       = 15;
        _maxSightDistance = 10;
    }


    void Update()
    {
        RotateView();
        MovePlayer();
        Jump();
        Look();
    }

    private void RotateView()
    {
        _rotateX                = Input.GetAxis("Mouse X") * _sensitivity;
        _rotateY                = Input.GetAxis("Mouse Y") * _sensitivity;
        _cameraRotation        -= _rotateY;
        _cameraRotation         = Mathf.Clamp(_cameraRotation, -50f, 50f);

        GetComponent<Transform>().Rotate(Vector3.up * _rotateX);

        Camera.transform.localRotation = Quaternion.Euler(_cameraRotation, 0f, 0f);
    }

    private void MovePlayer()
    {
        _moveX       = Input.GetAxis("Horizontal");
        _moveY       = Input.GetAxis("Vertical");
        Vector3 move = GetComponent<Transform>().right * _moveX + GetComponent<Transform>().forward * _moveY;

        GetComponent<CharacterController>().Move(move * _moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _onGround)
        { 
            _onGround   = false;
            _velocity.y = Mathf.Sqrt(_jumpHeight * _gravity);
        }
        _velocity.y -= _gravity * Time.deltaTime;
        GetComponent<CharacterController>().Move(_velocity * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground" && !_onGround) _onGround = true;
    }

    private void Look()
    {

        RaycastHit hit;
        int layerMask = 1 << 10;
        layerMask = ~layerMask;
        Interactable interactable;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, _maxSightDistance, layerMask)
                && (interactable = hit.collider.gameObject.GetComponent<Interactable>()) != null)
        {
            Debug.Log("gledam u nešto");
            interactable.ShowText();
            if (Input.GetKeyDown(KeyCode.E))
            {
               interactable.Interact();
            }
        }
        else
        {
            CanvasManager.GetComponent<CanvasManager>().HideMessage();
        }

    }
}
