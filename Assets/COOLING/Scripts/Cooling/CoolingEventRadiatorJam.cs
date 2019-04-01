using UnityEngine;

namespace AISIS.Games.Cooling
{
    /// Represents a radiator jam event that switches off the radiator functionality on start up.
    /// Can be repaired (destroyed) by the player. Is optically represented by a particle system.
    public class CoolingEventRadiatorJam : MonoBehaviour
    {
        public string popUpText;
        public GameObject floatingText;

        private CoolingRadiatorScript radiatorScript;

        void Start()
        {
            radiatorScript = GameObject.Find("Radiator").GetComponent<CoolingRadiatorScript>();
            radiatorScript.SwitchOff();
            radiatorScript.SetIsJammed(true);
        }

        /// <summary>
        /// When the player interacts with this event, he repairs the radiator jam and the event object
        /// gets destroyed. Also it pops up a feedback text.
        /// </summary>
        /// <param name="_Player">Interacting player</param>
        void Activated(GameObject _Player)
        {
            this.gameObject.SetActive(false);
            if(floatingText){
                ShowFloatingText(popUpText);
            }
            _Player.SendMessage("deleteInteractableObject", gameObject);
            radiatorScript.SetIsJammed(false);
            Destroy(gameObject);
        }
        
        /// <summary>
        /// Pop up a floating text aligned to screen
        /// </summary>
        /// <param name="_Text">Containing text</param>
        void ShowFloatingText(string _Text)
        {
           Quaternion camDirection = transform.rotation;
           camDirection = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles);
           var clone = Instantiate(floatingText, transform.position, camDirection);
           clone.GetComponentInChildren<TextMesh>().text = _Text;
        }

        /// <summary>
        /// Function called by the Player
        /// </summary>
        void InteractionRequest(GameObject _Player)
        {
            // return Message to the player with this gameObject
            _Player.SendMessage("addInteractableObject", gameObject);
        }
    }
}
