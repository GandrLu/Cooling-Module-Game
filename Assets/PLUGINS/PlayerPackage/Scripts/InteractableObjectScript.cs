using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
    // function called by the Player
    void InteractionRequest(GameObject player)
    {
        // return Message to the player with this gameObject
        player.SendMessage("addInteractableObject", gameObject);
    }
}
