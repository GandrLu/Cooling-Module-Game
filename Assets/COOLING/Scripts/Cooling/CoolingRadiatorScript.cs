using UnityEngine;
using UnityEngine.UI;

namespace AISIS.Games.Cooling
{
    /// Handles the functionality of the radiator. The radiator lets the player control the heat reducement of
    /// the molecules. It consumes power and can be jammed.
    public class CoolingRadiatorScript : MonoBehaviour
    {
        public bool isJammed = false;
        public int powerConsume;
        public int temperatureReducementFactor = 20;
        public GameObject[] lamps;
        public CoolingSpawnScript spawn;

        private short powerLevel;
        private short MAXPOWERLEVEL = 3;
        private Text radiatorPowerText;
        private Animator animator;
        private CoolingPowerManager powerManager;
        
        void Start()
        {
            animator = GetComponent<Animator>();
            spawn = GetComponent<CoolingSpawnScript>();
            powerManager = GameObject.Find("CoolingGameManager").GetComponent<CoolingPowerManager>();
            radiatorPowerText = GameObject.Find("RadiatorPowerText").GetComponent<Text>();
            // Initialize starting state
            animator.SetFloat("animSpeed", 1.0f);
            lamps[0].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            lamps[1].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            powerLevel = 2;
            powerConsume = powerLevel * 30;
            spawn.temperatureAmount = powerLevel * -temperatureReducementFactor;

            powerManager.UpdatePowerConsume('R', powerConsume);
        }

        private void Update()
        {

        }

        /// <summary>
        /// On interaction (if not jammed) the radiator increases its power level and adjusts the optical representation (animation 
        /// and amount of enabled lamps) as well as the temperatur reducement at spawn and the power consumption. If the 
        /// maximum power level is running, it switches off and increases again on next interaction.
        /// </summary>
        void Activated()
        {
            if(!isJammed)
            {
                if(powerLevel == MAXPOWERLEVEL)
                {
                    this.SwitchOff();
                }
                else
                {
                    ++powerLevel;
                    switch (powerLevel)
                    {
                        case 1:
                            animator.SetFloat("animSpeed", 0.5f);
                            lamps[0].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            break;
                        case 2:
                            animator.SetFloat("animSpeed", 1.0f);
                            lamps[1].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            break;
                        case 3:
                            animator.SetFloat("animSpeed", 1.5f);
                            lamps[2].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            break;
                        default:
                            animator.SetFloat("animSpeed", 1.0f);
                            lamps[0].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            lamps[1].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                            lamps[2].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                            break;
                    }
                    powerConsume = powerLevel * 30;
                    spawn.temperatureAmount = powerLevel * -temperatureReducementFactor;
                    powerManager.UpdatePowerConsume('R', powerConsume);
                }
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
            powerConsume = powerLevel * 30;
            spawn.temperatureAmount = powerLevel * -temperatureReducementFactor;
            powerManager.UpdatePowerConsume('R', powerConsume);
        }

        /// <summary>
        /// Called by the jam event. Jams or unjams the radiator, in case of unjamming, the HUD
        /// text color gets white and the activated method is called to get back on track.
        /// In case of jamming the HUD text gets red.
        /// </summary>
        /// <param name="isJammed">Defines jam state</param>
        public void SetIsJammed(bool isJammed)
        {
            this.isJammed = isJammed;
            switch (isJammed)
            {
                case true:
                    radiatorPowerText.color = Color.red;
                    break;
                case false:
                    radiatorPowerText.color = Color.white;
                    Activated();
                    break;
                default:
                    radiatorPowerText.color = Color.white;
                    Activated();
                    break;
            }
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