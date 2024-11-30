using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private float playerSpeed = 6.0f;

    private bool isCrouching = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Keyboard Inputs
        if (Input.GetKey(KeyCode.W))
        {
            // move the player relative to the game object
            player.transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleCrouch();
        }
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
