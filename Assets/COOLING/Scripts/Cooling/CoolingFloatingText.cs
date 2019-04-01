using UnityEngine;

namespace AISIS.Games.Cooling
{
    /// Simple script to destroy a spawned floating text after certain time and updates the 
    /// rotation to keep it screen oriented on each frame.
    public class CoolingFloatingText : MonoBehaviour
    {
        public float DestroyTime = 3f;

        void Start()
        {
            Destroy(gameObject, DestroyTime);
        }

        void Update()
        {
            transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles);
        }
    }
}
