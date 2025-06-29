using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the offset based on the initial positions of the camera and the player
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the camera's position to follow the player
        transform.position = player.transform.position + offset;
    }
}
