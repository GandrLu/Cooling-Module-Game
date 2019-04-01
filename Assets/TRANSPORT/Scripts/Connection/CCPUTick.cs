using UnityEngine;

namespace AISIS.transport
{
    public class CCPUTick : MonoBehaviour
    {
        public bool CPUTICK = false;
        //Ticktime of the CPU
        public float tickTime = 1.0f;
        public Material[] material;
        //Getting Tick for the CPU
        public bool getTick()
        {
            return this.CPUTICK;
        }
        //Getting Materials for the Connectors
        public Material[] getMaterials()
        {
            return material;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Renderer rend = GetComponent<Renderer>();
            if (CPUTICK == false)
            {
                rend.sharedMaterial = material[0];
            }
            else
            {
                rend.sharedMaterial = material[1];
            }
            tickTime -= Time.deltaTime;
            if (tickTime < 0)
            {
                CPUTICK = !CPUTICK;
                tickTime = 1.0f;
            }
        }
    }
}
