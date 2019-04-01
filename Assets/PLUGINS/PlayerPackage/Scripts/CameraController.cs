using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // public variable for debugging
    public float speed = 2.0f;

    // public variable for GameObjects
    public GameObject player;
    public GameObject actionButton;

    // private variable
    private float Y;
    private float X;

    


    // Update is called once per frame
    void Update()
    {
        // Rotate camera around while pressing the right mouse button
        if (Input.GetButton("Free Camera"))
        {
            actionButton.SetActive(false);
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
        // Look at the Player
        else
        {
            transform.LookAt(player.transform);
        }
    }
}
