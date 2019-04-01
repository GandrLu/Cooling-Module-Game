using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePickUpDropScript : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {

        string cTag = collider.gameObject.tag;

        // if the Player enters the Trigger of the Drop object, the function sends DropObject with itself as Parameter to the player ( if you have different dropObjects and need to know with one is triggered)
        if (cTag == "Player")
        {
            collider.gameObject.SendMessage("DropObject", gameObject);
        }


    }
}
