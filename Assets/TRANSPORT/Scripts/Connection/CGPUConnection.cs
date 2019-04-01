using System.Collections.Generic;
using UnityEngine;

namespace AISIS.transport
{
    public class CGPUConnection : MonoBehaviour
    {
        public double voltage = 0.0;
        public double temperature = 0.0;
        public Material[] material;

        public GameObject CPUTick;

        bool Tick = false;
        public List<CNibble> inputNumbers = new List<CNibble>();
        //Boolean for the KIOutput
        public bool KIOutput = false;
        //Function for an simple kioutput
        void GPUKi()
        {
            inputNumbers.RemoveAt(inputNumbers.Count - 1);
        }
        //Add Voltage for the Connector 
        public void addVoltage(double _value)
        {
            this.voltage += _value;
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
        //Adds temperatur to the Connector
        public void addTemperatur(double _value)
        {
            this.temperature += _value;
        }
        //Insert number to the inputstack
        public void insertNumber(CNibble _value)
        {
            inputNumbers.Add(_value);
        }

        // Start is called before the first frame update
        void Start()
        {
            //Getting Gameobjects
            CPUTick = GameObject.Find("CPUTick");
            material = CPUTick.GetComponent<CCPUTick>().getMaterials();
        }

        // Update is called once per frame
        void Update()
        {
            Renderer rend = GetComponent<Renderer>();
            Tick = CPUTick.GetComponent<CCPUTick>().getTick();
            //If Tick is true then numbers will be deleted
            if (Tick == false)
            {
                rend.sharedMaterial = material[0];
            }
            else
            {
                rend.sharedMaterial = material[1];
                if (KIOutput == true)
                {
                    GPUKi();
                }
            }
        }
    }
}