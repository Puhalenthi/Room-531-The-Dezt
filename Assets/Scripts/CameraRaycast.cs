using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycast : MonoBehaviour
{
    [Header("Raycast Features")]
    [SerializeField] private float rayLength = 5.0f;
    private Camera _camera;
    
    //private DeztController _deztController;
    
    [Header("Interaction")]
    [SerializeField] private KeyCode interactionKey;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, rayLength))
        {
            if (hit.collider != null)
            {
                Debug.Log(new Vector3(0.5f, 0.5f));
                Debug.DrawLine(_camera.transform.position, _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), Color.white, 100f);
                Debug.Log(hit.collider);
            }
        }
    }
}
