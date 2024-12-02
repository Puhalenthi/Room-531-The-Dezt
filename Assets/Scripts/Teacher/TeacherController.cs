using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TeacherMovement : MonoBehaviour
{
    public Teacher TeacherType;

    public GameObject Player;
    private Transform _playerTransform;
    public GameObject DoorWay;
    private Transform _doorWayTransform;

    private AudioSource _audioSource;
    private Rigidbody _teacherRigidBody;

    private float _currentMovementSpeed;
    private Vector3 _direction;


    public float AgroWaitTime;
    private bool _followPlayer;
    private bool _canDespawn;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = Player.GetComponent<Transform>();
        _doorWayTransform = DoorWay.GetComponent<Transform>();
        _audioSource = GetComponent<AudioSource>();
        _teacherRigidBody = GetComponent<Rigidbody>();

        transform.localScale = new Vector3(TeacherType.scale, TeacherType.scale, TeacherType.scale);
        
        _followPlayer = true;
        _canDespawn = false;
        _currentMovementSpeed = 0;
        StartCoroutine("JumpScareTimeout");
    }

    // Update is called once per frame
    void Update()
    {
        if (_followPlayer)
        {
            _direction = new Vector3(_playerTransform.position.x - transform.position.x, 0, _playerTransform.position.z - transform.position.z).normalized;
        } else
        {
            _direction = new Vector3(_doorWayTransform.position.x - transform.position.x, 0, _doorWayTransform.position.z - transform.position.z).normalized;
        }
        _teacherRigidBody.MovePosition(transform.position + _direction * _currentMovementSpeed * Time.deltaTime);

        if (_canDespawn)
        {
            if (System.Math.Abs(transform.position.x - _doorWayTransform.position.x) < 1 && System.Math.Abs(transform.position.z - _doorWayTransform.position.z) < 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
        // Detects of the teacher collides with an empty desk or the player
    {
        if (collision.gameObject.CompareTag("HidingDesk"))
        {
            StartCoroutine("WaitToLeavePlayer");
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            TeacherType.KillPlayerAction(_audioSource);
            _currentMovementSpeed = 0;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("HidingDesk"))
        {
            StopCoroutine("WaitToLeavePlayer");
        }
    }

    public void OnTriggerEnter(Collider other)
        // Despawns teacher if exits the room
    {
        if (other.gameObject.CompareTag("Doorway"))
        {
            if (_canDespawn)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Doorway"))
        {
            //canDespawn = true;
        }
    }

    IEnumerator WaitToLeavePlayer()
    {
        yield return new WaitForSeconds(AgroWaitTime);
        _followPlayer = false;
        _currentMovementSpeed = TeacherType.movementSpeed;
    }

    IEnumerator JumpScareTimeout()
    {
        yield return new WaitForSeconds(2);
        _currentMovementSpeed = TeacherType.movementSpeed;
    }
}
