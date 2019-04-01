using UnityEngine;

namespace AISIS.Games.Cooling
{
	/// Represents a pipe break event that destroys CWater instances on trigger enter with a probability
    /// defined by leakageRate. Can be repaired (destroyed) by the player. Is optically represented by
    /// a particle system that is randomly rotated.
    public class CoolingEventPipeBreak : MonoBehaviour
    {
        public float leakageRate = 0.3f;
        public string popUpText;
        public GameObject floatingText;

        private ParticleSystem particleSystem;
        private CoolingMoleculeManager moleculeManager;

        void Start()
        {
            particleSystem = GetComponent<ParticleSystem>();
            moleculeManager = GameObject.Find("CoolingGameManager").GetComponent<CoolingMoleculeManager>();
            // Set random rotation on x axis for particle system
            Vector3 rotation = new Vector3(Random.Range(-200, 20), 0, 0);
            var shape = particleSystem.shape;
            shape.rotation = rotation;
        }

        /// <summary>
        /// When a molecule enters the trigger and a random value is smaller than the leakage value, 
        /// the molecule gets destroyed and it is removed from the gameManagers moleculeList.
        /// </summary>
        /// <param name="_Other">Other collider</param>
        void OnTriggerEnter(Collider _Other)
        {
            if (_Other.tag == "Cooling.Collectable")
            {
                if(Random.Range(0f, 1f) <= leakageRate){
                    if(moleculeManager.moleculeList.Remove(_Other.gameObject.GetComponent<CWater>()));
                    Destroy(_Other.gameObject);
                }
            }
        }

        /// <summary>
        /// When the player interacts with this event, he repairs the pipe break and the event object
        /// gets destroyed. Also it pops up a feedback text.
        /// </summary>
        /// <param name="_Player">Interacting player</param>
        void Activated(GameObject _Player)
        {
            this.gameObject.SetActive(false);
            if(floatingText)
			{
                ShowFloatingText(popUpText);
            }
            _Player.SendMessage("deleteInteractableObject", gameObject);
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
