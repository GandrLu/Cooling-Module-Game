using System;
using UnityEngine;

namespace AISIS.Games.Cooling
{
    /// Spawns molecules (CWater instances) and manipulates their values (temperature, next waypoint). Is attached to every component that interrupts the floating pipes.
    /// In case of components that have more than one outgoing direction, it divides the outgoing molecules evenly.
    public class CoolingSpawnScript : MonoBehaviour
    {
        // Amount to change on every spawned molecule (increase or decrease)
        public int temperatureAmount;
        // Rate of molecule spawns
        public float rateMoleculeSpawn = 0.2f;
        // A const used to get a random spawn coordinate
        public const float SPAWNAREA = 1.0f;
        public CWater prefabObject;
        public GameObject floatingText;
        public GameObject[] waypoints;

        // Indicates if this spawn has only one or two outgoing directions
        bool singlespawn = true;
        // Timer for molecule spawn rate
        private float timerMolecules = 0.0f;
        // Following four variables are used for multiple outgoing directions
        private int spawnIndex = 0;
        private int spawnToggle = 1;
        // Positions where will be spawned
        private Component[] spawnPositions;
        private CoolingCollectorScript collector;
        private CoolingMoleculeManager moleculeManager;

        /// <summary>
        /// Finds all as spawn tagged objects and fills the molecule queue of the attached collector with molecules
        /// for the start.
        /// </summary>
        void Start()
        {
            collector = GetComponent<CoolingCollectorScript>();
            moleculeManager = GameObject.Find("CoolingGameManager").GetComponent<CoolingMoleculeManager>();
            // Find all as spawn tagged objects
            spawnPositions = ExtensionMethods.FindComponentsInChildrenWithTag<Component>(gameObject , "Cooling.Spawn");

            if (collector && prefabObject && waypoints.Length > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    CWater clone = Instantiate(prefabObject);
                    // Wapoint doesnt matter, gets overwritten later, and index 0 is always available
                    clone.waypoint = waypoints[0];
                    moleculeManager.AddMoleculeToList(clone);
                    clone.gameObject.SetActive(false);
                    collector.moleculeQueue.Enqueue(clone);
                }
            }

            if (spawnPositions.Length > 1)
            {
                singlespawn = false;
            }
        }

        /// <summary>
        /// On each frame molecules are spawned according to the spawn timer, unless the 
        /// queue is empty. Each molecule gets a spawnpoint that is randomly modified within a
        /// certain factor. Also its temperature is changed and a new waypoint is attached.
        /// In the end it is set active, while in queue all molecules are inactive.
        /// </summary>
        void Update()
        {
            timerMolecules += Time.deltaTime;
            if (collector.moleculeQueue.Count > 0 && timerMolecules > rateMoleculeSpawn)
            {
                CWater spawnMolecule = collector.moleculeQueue.Dequeue();
                float randomY = UnityEngine.Random.Range(-(SPAWNAREA), SPAWNAREA);
                float randomZ = UnityEngine.Random.Range(-(SPAWNAREA), SPAWNAREA);
                Vector3 spawnPos = GetSpawnPosition();
                spawnPos.y += randomY;
                spawnPos.z += randomZ;
                spawnMolecule.transform.position = spawnPos;
                spawnMolecule.ChangeTemperatur(temperatureAmount);
                if (floatingText)
                {
                    ShowFloatingText(temperatureAmount);
                }
                spawnMolecule.waypoint = GetWaypoint(); ;
                spawnMolecule.gameObject.SetActive(true);

                timerMolecules -= rateMoleculeSpawn;
            }
        }

        /// <summary>
        /// Returns alternately a position when this component has two spawns.
        /// When not it returns the only available position.
        /// </summary>
        /// <returns>A vector as spawn position</returns>
        Vector3 GetSpawnPosition()
        {
            if (singlespawn)
            {
                return spawnPositions[0].transform.position;
            }
            else
            {
                // Swap between 0 and 1 by adding -1 or 1
                spawnIndex += spawnToggle;
                spawnToggle = -spawnToggle;
                try
                {
                    return spawnPositions[spawnIndex].transform.position;
                }
                catch (Exception e)
                {
                    if (e.Source != null)
                        Console.WriteLine("Exception source: {0}", e.Source);
                    throw;
                }
            }
        }

        /// <summary>
        /// Returns alternately a waypoint, according to the chosen spawnpoint.
        /// </summary>
        /// <returns></returns>
        GameObject GetWaypoint()
        {
            if (singlespawn)
            {
                return waypoints[0];
            }
            else
            {
                try
                {
                    return waypoints[spawnIndex];
                }
                catch (Exception e)
                {
                    if (e.Source != null)
                        Console.WriteLine("Exception source: {0}", e.Source);
                    throw;
                }
            }
        }

        /// <summary>
        /// Adds a new molecule (CWater instance) to the attached collector molecule queue
        /// </summary>
        public void AddMoleculeToQueue()
        {
            CWater clone = Instantiate(prefabObject);
            clone.waypoint = waypoints[0];
            moleculeManager.AddMoleculeToList(clone);
            clone.gameObject.SetActive(false);
            collector.moleculeQueue.Enqueue(clone);
        }

        /// <summary>
        /// Pop up a floating text aligned to screen
        /// </summary>
        /// <param name="_Text">Containing text</param>
        void ShowFloatingText(int _Text)
        {
           Quaternion camDirection = transform.rotation;
           camDirection = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles);
           var clone = Instantiate(floatingText, transform.position + new Vector3(0, 10, 0), camDirection);
           clone.GetComponentInChildren<TextMesh>().text = _Text.ToString();
        }
    }
}
