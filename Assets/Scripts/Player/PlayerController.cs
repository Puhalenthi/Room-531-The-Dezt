using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private float playerSpeed = 1.0f;
    private float playerRotationSpeed = 1.0f;

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

        //use quaternion to rotate the player based on mouse movement
        float horizontal = Input.GetAxis("Mouse X") * playerRotationSpeed;
        float vertical = Input.GetAxis("Mouse Y") * playerRotationSpeed;
        player.transform.Rotate(vertical, horizontal, 0);


    }
}
