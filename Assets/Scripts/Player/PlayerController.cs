using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    
    private float _playerWalkingSpeed = 2.0f;
    private float _playerCrouchingSpeed = 1.0f;

    private Rigidbody _playerRigidbody;

    private bool _isCrouching = false;
    public static bool IsHiding { get; private set; }
    public GameObject CurrentHidingDesk { get; private set;}

    public static GameObject playerDezt { get; set; }

    public static bool IsSitting { get; private set; }

    private bool _isCollidingPlayerDesk = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = player.GetComponent<Rigidbody>();
        IsHiding = true;
        IsSitting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleCrouch();
        }
        if ((_isCollidingPlayerDesk || IsSitting) && Input.GetKeyDown(KeyCode.N))
        {
            IsSitting = !IsSitting;
            _isCrouching = false;
            playerDezt.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (IsSitting)
        {
            player.transform.position = new Vector3(11.25f, 1f, 10.92f);
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            playerDezt.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            
        }

    }
    void FixedUpdate()
    {
        if (!IsSitting)
        {
            float speed = _isCrouching ? _playerCrouchingSpeed : _playerWalkingSpeed;
            Vector3 movement = Vector3.zero;

            // Keyboard Inputs
            if (Input.GetKey(KeyCode.W))
            {
                movement += player.transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movement += -player.transform.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movement += -player.transform.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movement += player.transform.right;
            }

            _playerRigidbody.MovePosition(player.transform.position + movement * Time.deltaTime * speed);
        }

    }

    void ToggleCrouch()
    {
        if (_isCrouching)
        {
            _isCrouching = false;
            player.transform.Translate(new Vector3(0, +0.5f, 0));
        }
        else
        {
            _isCrouching = true;
            player.transform.Translate(new Vector3(0, -0.5f, 0));
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerDesk"))
        {
            _isCollidingPlayerDesk = true;
        }
    }
    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerDesk"))
        {
            _isCollidingPlayerDesk = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HidingTrigger"))
        {
            IsHiding = true;
            CurrentHidingDesk = other.gameObject.transform.parent.gameObject;
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("HidingTrigger"))
        {
            IsHiding = false;
            CurrentHidingDesk = null;
        }
    }


}
