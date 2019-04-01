using UnityEngine;

namespace AISIS.Games.Cooling
{
    /// Handles the functionality of the tank. The tank lets the player add new molecules (CWater instances)
    /// to the cooling circuit.
    public class CoolingTankScript : MonoBehaviour
    {
        // Maximum of molecules in the circuit
        public int MAXCOUNTMOLECULES;
        public string popUpText = "+1";
        public Renderer[] lamps;
        public GameObject floatingText;

        private CoolingSpawnScript spawnScript;
        private CoolingMoleculeManager gameManager;

        void Start()
        {
            MAXCOUNTMOLECULES = 200;
            spawnScript = GetComponent<CoolingSpawnScript>();
            gameManager = GameObject.Find("CoolingGameManager").GetComponent<CoolingMoleculeManager>();
            lamps = ExtensionMethods.FindComponentsInChildrenWithTag<Renderer>(gameObject, "Cooling.Lamp");
        }

        /// <summary>
        /// Calculate how many lamps should be lighted, according to the filling level of the coolin circuit.
        /// </summary>
        void Update()
        {
            float factor = (float)gameManager.moleculeList.Count / (float)MAXCOUNTMOLECULES;
            factor = factor * lamps.Length;
            for (int i = 0; i < (int)factor; i++)
            {
                lamps[i].material.SetColor("_EmissionColor", Color.green);
            }
            for (int i = (int)factor; i < lamps.Length-1; i++)
            {
                lamps[i].material.SetColor("_EmissionColor", Color.blue);
            }
        }

        /// <summary>
        /// On interaction a new molecule is filled into the circuit.
        /// </summary>
        void Activated()
        {
            if (gameManager.moleculeList.Count < MAXCOUNTMOLECULES)
            {
                spawnScript.AddMoleculeToQueue();
                ShowFloatingText();
            }
        }

        /// <summary>
        /// Pop up a floating text aligned to screen
        /// </summary>
        void ShowFloatingText()
        {
            Quaternion camDirection = transform.rotation;
            camDirection = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles);
            var clone = Instantiate(floatingText, transform.position + new Vector3(0, 10, 0), camDirection);
            clone.GetComponentInChildren<TextMesh>().text = popUpText;
        }

        /// <summary>
        /// Called by the player
        /// </summary>
        /// <param name="_Player"></param>
        void InteractionRequest(GameObject _Player)
        {
            // return Message to the player with this gameObject
            _Player.SendMessage("addInteractableObject", gameObject);
        }
    }
}