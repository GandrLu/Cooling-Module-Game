using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AISIS.Games.Cooling
{
    /// A manager class that collects all existent molecules at start up and holds a list of all 
    /// molecules in the scene. Keeps the HUD molecule text updated and changes the speed of all molecules.
    public class CoolingMoleculeManager : MonoBehaviour
    {
        private Text moleculesHUDText;
        public List<CWater> moleculeList = new List<CWater>();

        void Start()
        {
            moleculesHUDText = GameObject.Find("MoleculeCountText").GetComponent<Text>();
            // Collect all molecules
            GameObject[] moleculeArray = GameObject.FindGameObjectsWithTag("Cooling.Collectable");
            foreach (var molecule in moleculeArray)
            {
                moleculeList.Add(molecule.GetComponent<CWater>());
            }
        }

        void Update()
        {
            moleculesHUDText.text = "Molecules: " + moleculeList.Count.ToString();
        }

        /// <summary>
        /// Called by SpawnScript
        /// </summary>
        /// <param name="_Molecule">New molecule</param>
        public void AddMoleculeToList(CWater _Molecule)
        {
            moleculeList.Add(_Molecule);
        }

        public void ChangeMoleculeSpeed(float _NewSpeed)
        {
            foreach (CWater molecule in moleculeList)
            {
                molecule.changeSpeed(_NewSpeed);
            }
        }
    }
}