using UnityEngine;
using UnityEngine.UI;
using AISIS.transport;

namespace AISIS.Games.Cooling
{
    /// Handles behavior of heat producing components. Changes the heat level, plays a sound and changes 
    /// display text color while overheated, communicates the heat level with the global connection.
    public class CoolingHeatScript : MonoBehaviour
    {
        // Amount of heat that is added every time
        public int heatingAmount = 10;
        // Frequency of damage to player when overheated
        public float damageRate = 2.0f;
        // Current heat level
        public float heatLevelCurrent = 0;
        // Heat level that shall be reached next
        public float heatLevelTarget = 0;
        // Frequency of heating
        public float heatingRate = 0.1f;
        // Stepwidth of approximation from current to target heat
        public float heatingApproximationSteps = 0.2f;
        // Maximum of tolerable heat
        public float MAXHEAT = 100;
        public GameObject floatingText;

        // Timer for damage rate
        private float damageTimer = 0;
        // Timer for heating rate
        private float heatingTimer = -2.0f;
        private AudioSource audio;
        private Text displayText;
        // Display text not overheated
        private Color blueColor = new Color(0.2f, 0.75f, 1, 1);
        // Display text overheated
        private Color redColor = new Color(1, 0.2f, 0, 1);
        private CoolingGameOverManager gameOverManager;
        // Global connection
        private CCOOLINGConnection coolingConnection;

        void Start()
        {
            audio = GetComponent<AudioSource>();
            displayText = GetComponentInChildren<Text>();
            gameOverManager = GameObject.Find("Canvas").GetComponent<CoolingGameOverManager>();
            coolingConnection = GameObject.Find("COOLINGConnector").GetComponent<CCOOLINGConnection>();
        }

        void Update()
        {
            // Format display text to have three characters and add degree celsius sign
            displayText.text = heatLevelCurrent.ToString("000") + " °C";
            heatingTimer += Time.deltaTime;
            // Add heat amount
            if (heatingTimer > heatingRate)
            {
                heatLevelTarget += heatingAmount;
                coolingConnection.addTemperatur(heatingAmount);
                heatingTimer -= heatingRate;
            }
            // Approximate towards heat level target
            if(heatLevelTarget < heatLevelCurrent)
            {
                heatLevelCurrent -= heatingApproximationSteps;
            }
            // Approximate towards heat level target
            if (heatLevelTarget > heatLevelCurrent)
            {
                heatLevelCurrent += heatingApproximationSteps;
            }
            // In the lower three-quarters of max heat, do not play sizzling sound and make display text blue
            if (heatLevelCurrent < MAXHEAT * 0.75)
            {
                if (audio.isPlaying)
                {
                    audio.Stop();
                }
                displayText.color = blueColor;
            }
            // In the upper quarter of max heat, play sizzling sound and make display text red and start damaging player
            else
            {
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
                displayText.color = redColor;
                damageTimer += Time.deltaTime;

                if (damageTimer > damageRate)
                {
                    gameOverManager.ReduceHealth();
                    damageTimer -= damageRate;
                }
            }
        }

        /// <summary>
        /// Changes the heat level and pops the amount as text up and updates the global connection
        /// </summary>
        /// <param name="_amount">The amount that will be added or subtracted</param>
        public void reduceHeat(float _amount)
        {
            float reducement = heatLevelTarget - _amount;

            if(reducement > 0)
            {
                heatLevelTarget -= _amount;
                ShowFloatingText(_amount);
                coolingConnection.addTemperatur(-_amount);
            }
            else if(reducement < 0)
            {
                heatLevelTarget -= _amount + reducement;
                ShowFloatingText(_amount + reducement);
                coolingConnection.addTemperatur(-(_amount + reducement));
            }
            else if(reducement == 0)
            {
                heatLevelTarget -= _amount;
                ShowFloatingText(_amount);
                coolingConnection.addTemperatur(-_amount);
            }
        }

        /// <summary>
        /// Pop up a floating text aligned to screen
        /// </summary>
        /// <param name="_Text">Containing text</param>
        void ShowFloatingText(float _Text)
        {
            Quaternion camDirection = transform.rotation;
            camDirection = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles);
            var clone = Instantiate(floatingText, transform.position + new Vector3(0, 10, 0), camDirection);
            clone.GetComponentInChildren<TextMesh>().text = "+" + _Text.ToString();
        }
    }
}
