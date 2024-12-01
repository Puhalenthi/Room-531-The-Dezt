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
    private Transform playerTransform;
    private PlayerController playerController;
    public GameObject DoorWay;
    private Transform doorWayTransform;

    private AudioSource audioSource;
    private Rigidbody teacherRigidBody;

    private float currentMovementSpeed;
    private Vector3 direction;


    public float AgroWaitTime;
    public bool followPlayer;
    private bool canDespawn;

    void Awake()
    {
        followPlayer = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = Player.GetComponent<Transform>();
        playerController = Player.GetComponent<PlayerController>();
        doorWayTransform = DoorWay.GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        teacherRigidBody = GetComponent<Rigidbody>();

        transform.localScale = new Vector3(TeacherType.scale, TeacherType.scale, TeacherType.scale);

        canDespawn = false;
        currentMovementSpeed = 0;
        StartCoroutine("JumpScareTimeout");
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            direction = new Vector3(playerTransform.position.x - transform.position.x, 0, playerTransform.position.z - transform.position.z).normalized;
        }
        else
        {
            direction = new Vector3(doorWayTransform.position.x - transform.position.x, 0, doorWayTransform.position.z - transform.position.z).normalized;
        }
        teacherRigidBody.MovePosition(transform.position + direction * currentMovementSpeed * Time.deltaTime);

        /*
        if (canDespawn)
        {
            if (System.Math.Abs(transform.position.x - doorWayTransform.position.x) < 1 && System.Math.Abs(transform.position.z - doorWayTransform.position.z) < 1)
            {
                Destroy(gameObject);
            }
        }
        */
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
            TeacherType.KillPlayerAction(audioSource);
            currentMovementSpeed = 0;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("HidingDesk"))
        {
            //stops the teacher from teleporting into the player while they are hiding
            if (playerController.IsHiding && playerController.CurrentHidingDesk == collision.gameObject)
            {
                currentMovementSpeed = 0;
            }
            else
            {
                currentMovementSpeed = TeacherType.movementSpeed;
                StopCoroutine("WaitToLeavePlayer");
            }
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
            if (canDespawn)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Doorway"))
        {
            canDespawn = true;
        }
    }

    public void Despawn(float seconds)
    {
        StartCoroutine(WaitAndDespawn(seconds));
    }

    IEnumerator WaitToLeavePlayer()
    {
        yield return new WaitForSeconds(AgroWaitTime);
        followPlayer = false;
        currentMovementSpeed = TeacherType.movementSpeed;
    }

    IEnumerator JumpScareTimeout()
    {
        yield return new WaitForSeconds(2);
        currentMovementSpeed = TeacherType.movementSpeed;
    }

    IEnumerator WaitAndDespawn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }


}
