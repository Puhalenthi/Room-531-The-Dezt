using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    
    private float _playerWalkingSpeed = 1.5f;
    private float _playerCrouchingSpeed = 0.5f;

    private Rigidbody _playerRigidbody;

    private bool _isCrouching = false;
    public static bool IsHiding { get; private set; }
    private bool _hasDeztOpen = false;
    private GameObject _currentDezt = null;
    public GameObject CurrentHidingDesk { get; private set;}

    public static GameObject PlayerDezt { get; set; }
    public static List<GameObject> StudentDezts { get; set; }

    public static bool IsSitting { get; private set; }

    private bool _isCollidingPlayerDesk = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = player.GetComponent<Rigidbody>();
        IsHiding = false;
        IsSitting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && IsHiding == false)
        {
            ToggleCrouch();
        }

        for (int i=0; i<StudentDezts.Count; i++)
        {
            GameObject _dezt = StudentDezts[i];
            if (Math.Abs(transform.position.x - _dezt.transform.position.x) < 10f && 
                Math.Abs(transform.position.y - _dezt.transform.position.y) < 10f &&
                Math.Abs(transform.position.z - _dezt.transform.position.z) < 10f &&
                Input.GetKeyDown(KeyCode.R) && !_hasDeztOpen)
            {
                Debug.Log("MCMCOMOC");
                StudentDezts[i].gameObject.SetActive(true);
                _hasDeztOpen = true;
                Cursor.lockState = CursorLockMode.None;
                _currentDezt = StudentDezts[i];
            }
            else
            {
                //StudentDezts[i].gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && _hasDeztOpen) 
        {
            _hasDeztOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            _currentDezt.gameObject.SetActive(false);
            _currentDezt = null;
        }

        if ((_isCollidingPlayerDesk || IsSitting) && Input.GetKeyDown(KeyCode.R))
        {
            IsSitting = !IsSitting;
            _isCrouching = false;
            PlayerDezt.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (IsSitting)
        {
            player.transform.position = new Vector3(11.25f, 1f, 10.92f);
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            PlayerDezt.gameObject.SetActive(true);
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
            if (!Input.anyKeyDown)
            {
                _playerRigidbody.velocity = Vector3.zero;
            }
            if (!_hasDeztOpen)
            {
                _playerRigidbody.MovePosition(player.transform.position + movement * Time.deltaTime * speed);
            }
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
        if (other.gameObject.CompareTag("Teacher"))
        {
            StartCoroutine("deathDelay");
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

    IEnumerator deathDelay()
    {
        _playerWalkingSpeed = 0;
        _playerCrouchingSpeed = 0;
        yield return new WaitForSeconds(3);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("LoseScreen");
    }


}
