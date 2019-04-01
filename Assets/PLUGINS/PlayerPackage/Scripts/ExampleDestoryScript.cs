using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDestoryScript : MonoBehaviour
{
public void Activated(GameObject player)
    {
        //destory the object
        Destroy(gameObject);
    }
}
