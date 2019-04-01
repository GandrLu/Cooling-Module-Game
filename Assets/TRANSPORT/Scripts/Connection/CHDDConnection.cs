using System.Collections.Generic;
using UnityEngine;

namespace AISIS.transport
{
    public class CHDDConnection : MonoBehaviour
    {
        public double voltage = 0.0;
        public double temperature = 0.0;
        public Material[] material;

        public GameObject CPUTick;
        public GameObject GPU;

        bool Tick = false;

        public List<CNibble> inputNumbers = new List<CNibble>();
        public List<CNibble> outputNumbers = new List<CNibble>();
        //Boolean for the KIOutput
        public bool KIOutput = false;
        //Function for an simple kioutput
        void HDDKi()
        {
            CNibble nibble = inputNumbers[inputNumbers.Count - 1];
            inputNumbers.RemoveAt(inputNumbers.Count - 1);
            outputNumbers.Add(nibble);
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
        //Insert Number to the input storage
        public void insertNumber(CNibble _nibble)
        {
            inputNumbers.Add(_nibble);
        }
        //Get an number from the inputstack
        public CNibble getNumber()
        {
            CNibble nibble = new CNibble();
            nibble = inputNumbers[inputNumbers.Count - 1];
            inputNumbers.RemoveAt(inputNumbers.Count - 1);
            return nibble;
        }
        //Insert number to the outputstack
        public void setNumber(CNibble _value)
        {
            outputNumbers.Add(_value);
        }
        //Send a number to the GPU
        public void sendNumber()
        {
            CNibble outputNumber = outputNumbers[outputNumbers.Count - 1];
            outputNumbers.RemoveAt(outputNumbers.Count - 1);
            outputNumber.setDestination("GPU");
            GPU.GetComponent<CGPUConnection>().insertNumber(outputNumber);
        }
        // Start is called before the first frame update
        void Start()
        {
            //Getting Gameobjects
            GPU = GameObject.Find("GPUConnector");
            CPUTick = GameObject.Find("CPUTick");
            material = CPUTick.GetComponent<CCPUTick>().getMaterials();
        }

        // Update is called once per frame
        void Update()
        {
            Renderer rend = GetComponent<Renderer>();
            Tick = CPUTick.GetComponent<CCPUTick>().getTick();
            //If Tick is true then numbers will be send to the GPU
            if (Tick == false)
            {
                rend.sharedMaterial = material[0];
            }
            else
            {
                if (KIOutput == true)
                {
                    HDDKi();
                }
                rend.sharedMaterial = material[1];

            }
        }
    }
}