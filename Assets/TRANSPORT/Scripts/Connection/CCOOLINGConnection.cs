using UnityEngine;

namespace AISIS.transport
{
    public class CCOOLINGConnection : MonoBehaviour
    {
        public double voltage = 0.0;
        public double temperature = 0.0;
        public Material[] material;
        //Boolean for the KIOutput
        public bool KIOutput = false;

        public GameObject CPUTick;
        public GameObject RAM;
        public GameObject CPU;
        public GameObject HDD;
        public GameObject GPU;
        public GameObject POWER;

        bool Tick = false;

        bool oneTime = false;
        //Function for an simple kioutput
        void COOLINGKi()
        {
            addTemperatur(-10.0);
        }
        //Reduce Voltage of Connector
        public double reduceVoltage(double _value)
        {
            if (_value > this.voltage)
            {
                this.voltage -= _value;
                return _value;
            }
            else
            {
                double temp = this.voltage;
                this.voltage = 0;
                return temp;
            }
        }
        //Add Voltage for the Connector 
        public void addVoltage(double _value)
        {
            this.voltage += _value;
        }
        //Adds temperatur to the Connector
        public void addTemperatur(double _value)
        {
            this.temperature += _value;
        }

        // Start is called before the first frame update
        void Start()
        {
            //Getting Gameobjects
            RAM = GameObject.Find("RAMConnector");
            CPU = GameObject.Find("CPUConnector");
            HDD = GameObject.Find("HDDConnector");
            GPU = GameObject.Find("GPUConnector");
            POWER = GameObject.Find("POWERConnector");
            CPUTick = GameObject.Find("CPUTick");
//            material = CPUTick.GetComponent<CCPUTick>().getMaterials();
        }

        // Update is called once per frame
        void Update()
        {
            Renderer rend = GetComponent<Renderer>();
            // if(CPUTick){
            //     Tick = CPUTick.GetComponent<CCPUTick>().getTick();
            // }
            // //If Tick is true the temperatur will be split among the connectors
            // if (Tick == false)
            // {
            //     oneTime = false;
            //     rend.sharedMaterial = material[0];
            // }
            // else
            // {
            //     if (oneTime == false)
            //     {
            //         if (temperature < -10.0)
            //         {
            //             temperature = temperature + 10.0;
            //             // RAM.GetComponent<CRAMConnection>().addTemperatur(-2);
            //             // CPU.GetComponent<CCPUConnection>().addTemperatur(-2);
            //             // HDD.GetComponent<CHDDConnection>().addTemperatur(-2);
            //             // GPU.GetComponent<CGPUConnection>().addTemperatur(-2);
            //             // POWER.GetComponent<CPOWERConnection>().addTemperatur(-2);
            //         }
            //         else
            //         {
            //             if (temperature < 0)
            //             {
            //                 double temp = temperature;
            //                 temperature = 0;
            //                 // RAM.GetComponent<CRAMConnection>().addTemperatur(temp);
            //                 // CPU.GetComponent<CCPUConnection>().addTemperatur(temp);
            //                 // HDD.GetComponent<CHDDConnection>().addTemperatur(temp);
            //                 // GPU.GetComponent<CGPUConnection>().addTemperatur(temp);
            //                 // POWER.GetComponent<CPOWERConnection>().addTemperatur(temp);
            //             }
            //         }
            //         oneTime = true;
            //     }
            //     if (KIOutput == true)
            //     {
            //         COOLINGKi();
            //     }
            //     rend.sharedMaterial = material[1];
            // }
        }
    }
}
