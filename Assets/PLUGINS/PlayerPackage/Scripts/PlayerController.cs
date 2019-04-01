using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISIS.CharacterController
{
    // Script for controlling the character and the movement
    public class PlayerController : MonoBehaviour
    {

        // public variable for debugging
        public float walkingSpeed;
        public float rotationSpeed;
        public float jumpHeight;

        public bool EnableFlyMode;

        // public variable for GameObjects
        public GameObject actionButton;

        // variable for gameObjects etc.
        private Rigidbody rb;
        private GameObject currentPickUp;
        private Animator anim;


        // private variable
        private bool isGrounded = true;
        private float groundDistance = 1f;
        private List<GameObject> collidingObjectList = new List<GameObject>();
        private List<GameObject> deleteList = new List<GameObject>();

        // Start function
        void Start()
        {
            // set the rb Variable for later
            rb = GetComponent<Rigidbody>();

            // hide the Button for action
            actionButton.SetActive(false);

            // get the Animator
            anim = gameObject.GetComponentInChildren<Animator>();
            //Physics.gravity = new Vector3(0, 1F, 0);
        }

        // Update function
        void Update()
        {

            if (EnableFlyMode == false)
            {

                isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance);
                //Debug.Log(Physics.Raycast(transform.position, Vector3.down, groundDistance));
            }

            // checks if there are objects in the colliingObject list
            if (collidingObjectList.Count == 0)
            {
                actionButton.SetActive(false);
            } else
            {
                actionButton.SetActive(true);
            }


            foreach (GameObject collidingObject in collidingObjectList)
            {
                if (collidingObject == null)
                {
                    deleteList.Add(collidingObject);
                }
            }
                    // check for Interaction Button press and send Message 
                    if (Input.GetButtonDown("Interaction") && actionButton.activeSelf == true)
            {
                foreach (GameObject collidingObject in collidingObjectList) {

                        collidingObject.SendMessage("Activated", gameObject, SendMessageOptions.DontRequireReceiver);
                    
                }
            }

            // delete the objects from the collidingObjectList
            if (deleteList.Count != 0)
            {
                foreach (GameObject deleteObject in deleteList)
                {
                    collidingObjectList.Remove(deleteObject);
                }

                deleteList.Clear();
            }


            // function to remove the currentPickUp from the collidingObject list
            if (currentPickUp != null)
            {
                deleteInteractableObject(currentPickUp);
            }
            

        }

        // function is used when a object enters the trigger
        void OnTriggerEnter(Collider collider)
        {
            // if the colliding object is the currently hold pick up, the function will return
            if (collider.gameObject == currentPickUp)
            {
                return;
            }

            // send a Message InteractionRequest to the collider (is in the InteractableObjctScript)
            collider.SendMessage("InteractionRequest", gameObject, SendMessageOptions.DontRequireReceiver);
        }

        // function is used when a object stays in the trigger
        void OnTriggerStay(Collider collider)
        {
            // if the colliding object is the currently hold pick up, the function will return
            if (collider.gameObject == currentPickUp)
            {
                return;
            }

            // send a Message InteractionRequest to the collider (is in the InteractableObjctScript)
            collider.SendMessage("InteractionRequest", gameObject, SendMessageOptions.DontRequireReceiver);
        }

        // function is used when a object leaves the trigger
        void OnTriggerExit(Collider collider)
        {
            // send the colliding object to the deleteInteractableObject function to remove it from the objectfrom the interactableObjectList
            deleteInteractableObject(collider.gameObject);
        }

        // FixedUpdate is call before the Update function
        void FixedUpdate()
        {


            // rotate the character
            if (Input.GetButton("Horizontal"))
            {
                rb.transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
            }

            // Move character forward + Animation
            if (Input.GetButton("Vertical"))
            {
                rb.transform.localPosition += Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * walkingSpeed;
                anim.SetInteger("WalkPar", 1);
                /*
                 * Würde zwar so funktionieren, jedoch sollte man nicht an den velocity Werten manipulieren, da es zu unnatürlichen Bewegungen führt.
                 * In dem Fall würde der Spieler seine Höhe nicht verändern, auch wenn er gerade springt oder sich in der Luft befindet. 
                 * 
                Vector3 v3 = Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * walkingSpeed;
                rb.velocity = v3;
                */
            } else
            {
                anim.SetInteger("WalkPar", 0);
            }

            // Moves the current pick up object infront of the player + Animation
            if ( currentPickUp != null)
            {
                currentPickUp.transform.position = rb.transform.position + (rb.transform.forward * 3 / 2) + (rb.transform.up * 5 / 3);
                currentPickUp.transform.rotation = rb.transform.rotation;
                anim.SetInteger("HoldPar", 1);
            } else
            {
                anim.SetInteger("HoldPar", 0);
            }


            // get jump input
            if (Input.GetButton("Jump") && isGrounded)
            {
                anim.SetInteger("JumpPar", 1);
                Jump();
            } else
            {
                anim.SetInteger("JumpPar", 0);
            }

        }

        // Jump function
        void Jump()
        {
            //Vector3 jump = new Vector3(0.0f, 1.0f, 0.0f);
            //rb.AddForce(jump * jumpHeight, ForceMode.VelocityChange);
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        // function to add the gameObject to the collidingObjectList
        public void addInteractableObject(GameObject interactableObject)
        {
            // check if the object isn't in the list already
            if (collidingObjectList.Contains(interactableObject) == false)
            { 
            // add the object to the list
            collidingObjectList.Add(interactableObject);
            }
        }

        // function to save the Objects in a List to remove them after the Activated call
        public void deleteInteractableObject(GameObject interactableObject)
        {
            // check if the object is in the list
            if (collidingObjectList.Contains(interactableObject) == true)
            {
            // save the object in the deleteList
                deleteList.Add(interactableObject);
            }
        }

        // function to declare the gameObject as the currentPickUp
        public void PickUpObject(GameObject pickUpObject)
        {
            // check if the Player is holding another object
            if (currentPickUp == null)
            {
                // declare the object as the currentPickUp
                currentPickUp = pickUpObject;
            }
        }

        // removes the currentPickUp from the Player and sends the DropObject Message to the currentPickUp
        public void DropObject(GameObject dropObject)
        {
            // check if the player is holding something
            if (currentPickUp != null) {
            // send the currentPickUp the Message DropObject with the object that activated the DropObject function
            currentPickUp.SendMessage("DropObject", dropObject, SendMessageOptions.DontRequireReceiver);
            // remove the currentPickUp from the Player
            currentPickUp = null;
            }

        }
    }
}