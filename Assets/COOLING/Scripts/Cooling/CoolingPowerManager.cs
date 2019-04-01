using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AISIS.Games.Cooling
{
    /// A manager class that handles the power distribution, switches all consuming components off at overload
    /// and keeps the power HUD updated.
    public class CoolingPowerManager : MonoBehaviour
    {
        public float newpower;
        public float pumpPower;
        public float radiatorPower;
        public float totalPower = 200;
        public float totalPowerConsume;
        public string popUpText = "Power Overload!!!";
        public GameObject floatingText;
        public UnityEvent reducePowerConsume;

        private Text pumpPowerText;
        private Text radiatorPowerText;
        private Text totalPowerText;

        void Start()
        {
            pumpPowerText = GameObject.Find("PumpPowerText").GetComponent<Text>();
            radiatorPowerText = GameObject.Find("RadiatorPowerText").GetComponent<Text>();
            totalPowerText = GameObject.Find("TotalPowerText").GetComponent<Text>();
        }

        void Update()
        {
                pumpPowerText.text      =   "Pump Power: "      + pumpPower.ToString()      + "V";
                radiatorPowerText.text  =   "Radiator Power: "  + radiatorPower.ToString()  + "V";
                totalPowerText.text     =   "Total Power: "     + totalPower.ToString()     + "V";
        }

        /// <summary>
        /// Should normally be called by the global connection to use the power level of the global power module,
        /// but this is at the moment not implemented.
        /// When the new level would be lower than the current consumption, the overload switch off is triggered.
        /// </summary>
        /// <param name="_NewTotal">New total power level</param>
        void ChangeTotalPower(float _NewTotal)
        {
            totalPower = _NewTotal;
            if (totalPower < totalPowerConsume)
            {
                reducePowerConsume.Invoke();
            }
        }

        /// <summary>
        /// Updates the consumption of the components to be considered. And updates the total consumption 
        /// afterwards. When the new level is lower than the current total power, the overload switch off is triggered.
        /// </summary>
        /// <param name="_Identifier">Identifier of component</param>
        /// <param name="_NewConsume">New consumption</param>
        public void UpdatePowerConsume(char _Identifier, float _NewConsume)
        {
            switch (_Identifier)
            {
                case 'R':
                    radiatorPower = _NewConsume;
                    break;
                case 'P':
                    pumpPower = _NewConsume;
                    break;
            }
            totalPowerConsume = radiatorPower + pumpPower;
            if (totalPower < totalPowerConsume)
            {
                ShowFloatingText();
                reducePowerConsume.Invoke();
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
    }
}