using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AISIS.transport
{
    public class CRAMConnection : MonoBehaviour
    {
        public double voltage = 0.0;
        public double temperature = 0.0;
        public Material[] material;
        public List<string> outputNumbers;
        public GameObject CPUTick;
        public GameObject CPU;
        //Boolean for the KIOutput
        public bool KIOutput = false;

        bool Tick = false;

        //Function for an simple kioutput
        void RAMKi()
        {
                string result = "";
                for (int i = 0; i < 4; i++)
                {
                    int temp = Random.Range(0, 2);
                    if (temp > 0)
                    {
                        result += "1";
                    }
                    else
                    {
                        result += "0";
                    }
                }
                outputNumbers.Add(result);
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
        //Insert number to the outputnumbers
        public void insertNumber(string _value)
        {
            outputNumbers.Add(_value);
        }
        //Adds temperatur to the Connector
        public void addTemperatur(double _value)
        {
            this.temperature += _value;
        }
        //Send number to the CPU
        public void sendNumber()
        { 
            CPU.GetComponent<CCPUConnection>().insertNumber(outputNumbers[outputNumbers.Count - 1]);
            outputNumbers.RemoveAt(outputNumbers.Count - 1);
        }

        // Start is called before the first frame update
        void Start()
        {
            //Getting Gameobjects
            CPU = GameObject.Find("CPUConnector");
            CPUTick = GameObject.Find("CPUTick");
            material = CPUTick.GetComponent<CCPUTick>().getMaterials();
        }

        // Update is called once per frame
        void Update()
        {
            Renderer rend = GetComponent<Renderer>();
            Tick = CPUTick.GetComponent<CCPUTick>().getTick();  
            //If Tick is true then numbers will be send to the CPU
            if (Tick == false)
            {
                rend.sharedMaterial = material[0];
            }
            else
            {
                rend.sharedMaterial = material[1];
                if (KIOutput == true)
                {
                    RAMKi();
                }
                if (outputNumbers.Count > 1)
                {
                    sendNumber();
                }
            }
        }
    } }
