using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePickUpInteractionScript : MonoBehaviour
{
    // save the PlayerObject that is holding the Object
    private GameObject currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // function called by the player
    public void Activated(GameObject holdingPlayer)
    {
        // set the currentPlayer to the Player that send the Message
        currentPlayer = holdingPlayer;
        // send the Message to the PlayerObject to pickUp the object
        holdingPlayer.SendMessage("PickUpObject", gameObject);
    }

    // Message called by the player after triggering the Drop object
    public void DropObject(GameObject dropObject)
    {
        // Destroy the object after Droping it
        Destroy(gameObject);

    }
}
