using UnityEngine;

namespace AISIS.Games.Cooling
{
    /// Triggers game events (not unity events) in a certain frequency. It chooses between different kinds of
    /// events randomly. Also if possible, the location of the event is chosen randomly.
    public class CoolingEventManager : MonoBehaviour
    {
        public bool enableEvents = true;
        public float eventFrequency = 8.0f;
        public CoolingEventPipeBreak prefabPipeBreak;
        public CoolingEventRadiatorJam prefabRadiatorJam;
        public Transform[] eventLocations;

        private float timer = 0.0f;
        private CoolingRadiatorScript radiator;

        void Start(){
            radiator = GameObject.Find("Radiator").GetComponent<CoolingRadiatorScript>();
        }

        void Update()
        {
            if (enableEvents)
            {
                timer += Time.deltaTime;

                if (timer > eventFrequency)
                {
                    triggerEvent();
                    timer -= eventFrequency;
                }
            }
        }


        /// <summary>
        /// Returns a randomly chosen transform of the eventLocations array
        /// </summary>
        /// <returns>Transform of eventLocations</returns>
        Transform selectLocation()
        {
            return eventLocations[ new System.Random().Next(eventLocations.Length) ];
        }

        /// <summary>
        /// Triggers a event
        /// </summary>
        void triggerEvent()
        {
            int eventIdx = int.MaxValue;
            // When the radiator is not jammed, a random event is chosen, otherwise the pipeBreak is chosen. The random 
            // range is higher than the event amount to decrease the the chance of the radiator event.
            if(!radiator.isJammed)
            {
                eventIdx = Random.Range(0, 5);
            }
            switch (eventIdx)
            {
                case 0:
                        Instantiate(prefabPipeBreak, selectLocation());
                break;
                case 1:
                        // This event has only a default location
                        Instantiate(prefabRadiatorJam);
                break;
                default:
                        Instantiate(prefabPipeBreak, selectLocation());
                break;
            }
        }
    }
}
