using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePickUpTriggerScript : MonoBehaviour
{
    // save the PlayerObject that is holding the Object
    private GameObject currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = Color.yellow;
    }


    void OnTriggerEnter(Collider collider)
    {
        string cTag = collider.gameObject.tag;

        // if the Player enters the Trigger, the object will send the Message to be picked up by the Player
        if (cTag == "Player")
        {
        collider.SendMessage("PickUpObject", gameObject);
        }

    }

    // Message called by the player after triggering the Drop object
    public void DropObject(GameObject dropObject)
    {
        // Destroy the object after Droping it
        Destroy(gameObject);
    }
}
