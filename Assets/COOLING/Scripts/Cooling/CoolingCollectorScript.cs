using System.Collections.Generic;
using UnityEngine;

namespace AISIS.Games.Cooling
{
    /// Collects instances of CWater as molecules in a queue. When the trigger of this collector is entered by an object of 
    /// type "Cooling.Collectable", its CWater instance gets enqueued. Is attached to every component that interrupts the 
    /// floating pipes. In case this collector has a heatScript attached, the free temperature capacity of the molecule is 
    /// used to reduce the heat.
    public class CoolingCollectorScript : MonoBehaviour
    {
        public Queue<CWater> moleculeQueue = new Queue<CWater>();
        public CoolingHeatScript heatScript = null;

        void OnTriggerEnter(Collider _Other)
        {
            if (_Other.tag == "Cooling.Collectable")
            {
                // deactivate molecule object
                _Other.gameObject.SetActive(false);

                if(heatScript != null)
                {
                    // subtract temperature the molecule already has from its maximum to get free capacity
                    heatScript.reduceHeat(CWater.MAXTEMPERATURE - _Other.gameObject.GetComponent<CWater>().GetTemperature());
                }
                moleculeQueue.Enqueue(_Other.gameObject.GetComponent<CWater>());
            }
        }
    }
}
