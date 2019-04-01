using UnityEngine;
using UnityEngine.UI;

namespace AISIS.Games.Cooling
{
    /// A manager class that handles damage to the player, keeps the Health HUD updated and triggers a 
    /// game over feed back.
    public class CoolingGameOverManager : MonoBehaviour
    {
        // Variable for global game
        public bool gameLost = false;
        public int playerHealth = 150;
        public int healthReducement = 5;
        public float restartDelay = 5f;

        private Text gameOverText;
        private GameObject player;
        private Slider slider;
        // Timer to count up to restarting the level
        private float restartTimer;

        void Start()
        {
            gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
            player = GameObject.FindWithTag("Player");
            slider = GetComponentInChildren<Slider>();
            gameOverText.gameObject.SetActive(false);
            slider.value = playerHealth;
            slider.maxValue = playerHealth;
        }


        void Update()
        {
            // When health is empty, game is lost
            if (playerHealth <= 0)
            {
                gameOverText.gameObject.SetActive(true);
                if (player)
                {
                    player.SetActive(false);
                }
                gameLost = true;
                // Not used currently
                //restartTimer += Time.deltaTime;
                //
                //if (restartTimer >= restartDelay)
                //{
                //    Application.LoadLevel(Application.loadedLevel);
                //}
            }
        }

        /// <summary>
        /// Reduce health and update HUD slider
        /// </summary>
        public void ReduceHealth()
        {
            playerHealth -= healthReducement;
            slider.value = playerHealth;
        }
    }
}