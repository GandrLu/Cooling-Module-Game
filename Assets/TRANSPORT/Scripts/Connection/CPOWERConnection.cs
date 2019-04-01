using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISIS.transport
{
    public class CPOWERConnection : MonoBehaviour
    {
        public double voltage = 0.0;
        public double temperature = 0.0;
        public Material[] material;

        public GameObject CPUTick;
        public GameObject RAM;
        public GameObject CPU;
        public GameObject HDD;
        public GameObject GPU;
        public GameObject COOLING;

        bool Tick = false;
        //Boolean for the KIOutput
        public bool KiOutput = false;
        bool oneTime = false;
        //Function for an simple kioutput
        void POWERKi()
        {
            addVoltage(10.0);
        }
        //Adds temperatur to the Connector
        public void addTemperatur(double _value)
        {
            this.temperature += _value;
        }
        //Add Voltage for the Connector 
        public void addVoltage(double _value)
        {
            this.voltage += _value;
        }
        // Start is called before the first frame update
        void Start()
        {
            //Getting Gameobjects
            RAM = GameObject.Find("RAMConnector");
            CPU = GameObject.Find("CPUConnector");
            HDD = GameObject.Find("HDDConnector");
            GPU = GameObject.Find("GPUConnector");
            COOLING = GameObject.Find("COOLINGConnector");
            CPUTick = GameObject.Find("CPUTick");
            material = CPUTick.GetComponent<CCPUTick>().getMaterials();
        }

        // Update is called once per frame
        void Update()
        {
            Renderer rend = GetComponent<Renderer>();
            Tick = CPUTick.GetComponent<CCPUTick>().getTick();
            //If Tick is true Voltage will be spielt among all Connectors
            if (Tick == false)
            {
                oneTime = false;
                rend.sharedMaterial = material[0];
            }
            else
            {
                if(oneTime == false)
                {
                    if (voltage > 10)
                    {
                        voltage = voltage - 10.0;
                        RAM.GetComponent<CRAMConnection>().addVoltage(2);
                        CPU.GetComponent<CCPUConnection>().addVoltage(2);
                        HDD.GetComponent<CHDDConnection>().addVoltage(2);
                        GPU.GetComponent<CGPUConnection>().addVoltage(2);
                        COOLING.GetComponent<CCOOLINGConnection>().addVoltage(2);
                    }
                    else
                    {
                        double temp = voltage / 5.0;
                        voltage = 0;
                        RAM.GetComponent<CRAMConnection>().addVoltage(temp);
                        CPU.GetComponent<CCPUConnection>().addVoltage(temp);
                        HDD.GetComponent<CHDDConnection>().addVoltage(temp);
                        GPU.GetComponent<CGPUConnection>().addVoltage(temp);
                        COOLING.GetComponent<CCOOLINGConnection>().addVoltage(temp);
                    }
                    oneTime = true;
                }
                if(KiOutput == true)
                {
                    POWERKi();
                }
                rend.sharedMaterial = material[1];

            }
        }
    }
}


