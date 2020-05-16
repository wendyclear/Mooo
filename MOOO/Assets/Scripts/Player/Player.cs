using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    private float      _rotateX;
    private float      _rotateY;
    private float      _sensitivity;
    private float      _cameraRotation;
                       
    private float      _moveX;
    private float      _moveY;
    private float      _moveSpeed;
                       
    private float      _gravity;
    private float      _jumpHeight;
    private Vector3    _velocity;
    private bool       _onGround;
                       
    private int        _maxSightDistance;
                       
    private float      _hp;
    private bool       _alive;
    private bool       _paused;
                       
    public GameObject  bullet;
    public float       _bulletSpeed;
                       
    private int        _far = 1;
    public float       _up = 0;
    private int        _force = 80;
                       
    public GameObject  Camera;
    private GameObject _canvasManager;

    void Start()
    {
        _sensitivity      = 4f;
        _cameraRotation   = 0f;
        _moveSpeed        = 30f;
        _gravity          = 9.81f*6;
        _jumpHeight       = 15;
        _maxSightDistance = 10;
        _hp               = 100;
        _bulletSpeed      = 100;
        _alive            = true;
        _paused           = false;
        _canvasManager    = GameObject.Find("CanvasManager");
    }

    void Update()
    {
        if (!_paused)
        {
            RotateView();
            MovePlayer();
            Look();
            CheckHealth();
            Shoot();
            Jump();
        }
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
            interactable.ShowText();
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }
        else
        {
            _canvasManager.GetComponent<CanvasManager>().HideMessage();
        }

    }

    private void CheckHealth()
    {
        if (_hp <= 0)
        {
            _alive = false;
            _canvasManager.GetComponent<CanvasManager>().GameOver(_alive);
        }
    }

    public void Pause(bool paused)
    {
       _paused = paused;
        if (paused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(bullet) as GameObject;
            projectile.transform.position = Camera.transform.position * _far;
            projectile.GetComponent<Rigidbody>().velocity = Camera.transform.forward * _force;

        }
    }
    private void Freeze()
    {
        GetComponent<Rigidbody>().isKinematic = _paused;
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
    }
}
