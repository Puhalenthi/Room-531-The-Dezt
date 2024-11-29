using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TeacherMovement : MonoBehaviour
{
    public GameObject Player;
    private Transform playerTransform;
    public GameObject DoorWay;
    private Transform doorWayTransform;

    public float MovementSpeed;
    private float currentMovementSpeed;
    private Vector3 direction;


    public float AgroWaitTime;
    private bool followPlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = Player.GetComponent<Transform>();
        doorWayTransform = DoorWay.GetComponent<Transform>();
        followPlayer = true;
        currentMovementSpeed = 0;
        StartCoroutine("JumpScareTimeout");
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            direction = new Vector3(playerTransform.position.x - transform.position.x, 0, playerTransform.position.z - transform.position.z).normalized;
        } else
        {
            direction = new Vector3(doorWayTransform.position.x - transform.position.x, 0, doorWayTransform.position.z - transform.position.z).normalized;
        }
        transform.Translate(direction * currentMovementSpeed, Space.World);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HidingDesk"))
        {
            currentMovementSpeed = 0;
            StartCoroutine("WaitToLeavePlayer");
        }
    }

    IEnumerator WaitToLeavePlayer()
    {
        yield return new WaitForSeconds(AgroWaitTime);
        followPlayer = false;
        currentMovementSpeed = MovementSpeed;
    }

    IEnumerator JumpScareTimeout()
    {
        yield return new WaitForSeconds(2);
        currentMovementSpeed = MovementSpeed;
    }

}
