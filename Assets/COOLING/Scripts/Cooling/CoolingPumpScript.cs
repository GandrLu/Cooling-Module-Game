using UnityEngine;

namespace AISIS.Games.Cooling
{
    /// Handles the functionality of the pump. The pump lets the player control the floating speed of
    /// the molecules. It consumes power.
    public class CoolingPumpScript : MonoBehaviour
    {
        // The level the pump is running
        public short powerLevel;
        public int powerConsume;
        public GameObject[] lamps;

        private short MAXPOWERLEVEL = 4;
        private Animator animator;
        private CoolingMoleculeManager moleculeManager;
        private CoolingPowerManager powerManager;

        void Start()
        {
            animator = GetComponent<Animator>();
            GameObject manager = GameObject.Find("CoolingGameManager");
            moleculeManager = manager.GetComponent<CoolingMoleculeManager>();
            powerManager = manager.GetComponent<CoolingPowerManager>();
            // Initialize starting state
            animator.SetFloat("animSpeed", 0.6f);
            lamps[0].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            lamps[1].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            powerLevel = 2;
            powerConsume = powerLevel * 30;
            powerManager.UpdatePowerConsume('P', powerConsume);
        }

        /// <summary>
        /// On interaction the pump increases its power level and adjusts the optical representation (animation 
        /// and amount of enabled lamps) as well as the molecule speed and the power consumption. If the 
        /// maximum power level is running, it switches off and increases again on next interaction.
        /// </summary>
        void Activated()
        {
            if (powerLevel == MAXPOWERLEVEL)
            {
                this.SwitchOff();
            }
            else
            {
                ++powerLevel;
                switch (powerLevel)
                {
                    case 1:
                        animator.SetFloat("animSpeed", 0.3f);
                        lamps[0].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                        break;
                    case 2:
                        animator.SetFloat("animSpeed", 0.6f);
                        lamps[1].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                        break;
                    case 3:
                        animator.SetFloat("animSpeed", 1.2f);
                        lamps[2].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                        break;
                    case 4:
                        animator.SetFloat("animSpeed", 1.8f);
                        lamps[3].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                        break;
                    default:
                        animator.SetFloat("animSpeed", 1.0f);
                        break;
                }
                powerConsume = powerLevel * 30;
                powerManager.UpdatePowerConsume('P', powerConsume);
                moleculeManager.ChangeMoleculeSpeed(powerLevel);
            }
        }

        /// <summary>
        /// Sets the component to powered off state
        /// </summary>
        public void SwitchOff()
        {
            powerLevel = 0;
            animator.SetFloat("animSpeed", 0);
            lamps[0].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
            lamps[1].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
            lamps[2].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
            lamps[3].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
            powerConsume = powerLevel * 30;
            powerManager.UpdatePowerConsume('P', powerConsume);
            moleculeManager.ChangeMoleculeSpeed(powerLevel);
        }

        /// <summary>
        /// Function called by the Player
        /// </summary>
        void InteractionRequest(GameObject _Player)
        {
            // return message to the player with this gameObject
            _Player.SendMessage("addInteractableObject", gameObject);
        }
    }
}