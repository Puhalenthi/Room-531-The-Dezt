using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    
    private float playerWalkingSpeed = 2.0f;
    private float playerCrouchingSpeed = 1.0f;

    private Rigidbody playerRigidbody;

    private bool isCrouching = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleCrouch();
        }

    }
    void FixedUpdate()
    {
        float speed = isCrouching ? playerCrouchingSpeed : playerWalkingSpeed;
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

        playerRigidbody.MovePosition(player.transform.position + movement * Time.deltaTime * speed);
    }

    void ToggleCrouch()
    {
        if (isCrouching)
        {
            isCrouching = false;
            player.transform.Translate(new Vector3(0, +0.5f, 0));
        }
        else
        {
            isCrouching = true;
            player.transform.Translate(new Vector3(0, -0.5f, 0));
        }
    }
}
