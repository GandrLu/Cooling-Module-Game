using UnityEngine;

namespace AISIS.Games.Cooling
{
    /// A class that represents a fluid molecule and handles its behavior.
    public class CWater : MonoBehaviour
    {
        public float speed;
        // Waypoint the molecule shall move to
        public GameObject waypoint;

        public float temperature; //Temperature of the Water
        public const float MAXTEMPERATURE = 60; // Max. Temperature of the Water
        public const float MINTEMPERATURE = 0; // Max. Temperature of the Water
        public const float MAXSPEED = 4; // Max. Speed of the Water
        public const float MINSPEED = 0; // Max. Speed of the Water
        private Color color; //Color of the Water
        private Renderer renderer;

        void Start()
        {
            renderer = GetComponent<Renderer>();
            temperature = MINTEMPERATURE;
            speed = 3;
            updateColor();
        }

        void Update()
        {
            // Let the molecule move to its waypoint
            transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * speed);
        }

        /// <summary>
        /// Set absolute temperature.
        /// </summary>
        /// <param name="_temp">New temperature</param>
        public void SetTemperature(float _temp)
        {
            temperature = Mathf.Clamp(_temp, MINTEMPERATURE, MAXTEMPERATURE);
            updateColor();
        }

        /// <summary>
        /// Add or subtract a amount to/from temperature.
        /// </summary>
        /// <param name="_value">Amount to add/subtract</param>
        public void ChangeTemperatur(float _value)
        {
            temperature = Mathf.Clamp(temperature + _value, MINTEMPERATURE, MAXTEMPERATURE);
            updateColor();
        }

        public float GetTemperature()
        {
            return temperature;
        }

        public float getTemperatureNormalized()
        {
            return temperature.Map(MINTEMPERATURE, MAXTEMPERATURE, 0, 1);
        }

        public void changeSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        public Color getColor() {
            return color;
        } 

        /// <summary>
        /// Calculate the color by mapping the temperature to the hue of the color
        /// using this function: https://docs.unity3d.com/ScriptReference/Color.HSVToRGB.html
        /// </summary>
        void updateColor()
        {
            float hue = temperature.Map(MINTEMPERATURE, MAXTEMPERATURE, 0.7f, 0);
            //Debug.Log("updateColor" + hue);
            color = Color.HSVToRGB(hue, 1, 1);
            if (renderer)
            {
                renderer.material.SetColor("_Color", color);
            }
        }
    }
}