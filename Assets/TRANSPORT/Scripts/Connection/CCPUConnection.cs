using System;
using System.Collections.Generic;
using UnityEngine;

namespace AISIS.transport
{
    public class CCPUConnection : MonoBehaviour
    {

        public double voltage = 0.0;
        public double temperature = 0.0;
        public Material[] material;

        public GameObject CPUTick;
        public GameObject HDD;

        bool Tick = false;

        public List<CCPUTask> tasks = new List<CCPUTask>();
        public List<string> outputNumbers;
        public List<string> inputNumbers;
        //Boolean for the KIOutput
        public bool KIOutput = false;
        //Function for an simple kioutput
        void CPUKi()
        {
            string result = "";
            CCPUTask task = getTask();
            string number1 = task.getNumber1();
            string number2 = task.getNumber2();
            int value1FromTaskAsInt = Convert.ToInt32(number1, 2);
            int value2FromTaskAsInt = Convert.ToInt32(number2, 2);
            // Calculate the expected result.
            int expectedResult = value1FromTaskAsInt + value2FromTaskAsInt;
            result = expectedResult.ToString();
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
        //Adds temperatur to the Connector
        public void addTemperatur(double _value)
        {
            this.temperature += _value;
        }
        //Insert numbers to the inputstack
        public void insertNumber(string _value)
        {
            inputNumbers.Add(_value);
        }
        //Adds an number to the outputstack
        public void addNumberToQueue(string _value)
        {
            outputNumbers.Add(_value);
        }
        //Sends a number to the HDD
        public void sendNumber()
        {
            CNibble cNibble = new CNibble();
            string outputNumber = "";
            outputNumber = outputNumbers[outputNumbers.Count - 1];
            outputNumbers.RemoveAt(outputNumbers.Count - 1);
            cNibble.setString(outputNumber);
            cNibble.setDestination("HDD");
            HDD.GetComponent<CHDDConnection>().insertNumber(cNibble);
        }
        //Creates an task for the CPU
        public void createTask()
        {
            string calcOperator = "";
            string number1 = inputNumbers[inputNumbers.Count - 1];
            inputNumbers.RemoveAt(inputNumbers.Count - 1);
            string number2 = inputNumbers[inputNumbers.Count - 1];
            inputNumbers.RemoveAt(inputNumbers.Count - 1);

            System.Random random = new System.Random();
            int randomNumber = random.Next(0, 100);

            if (randomNumber > 49)
            {
                calcOperator = "+";
            }
            else
            {
                calcOperator = "-";
            }
            int value1FromTaskAsInt = Convert.ToInt32(number1, 2);
            int value2FromTaskAsInt = Convert.ToInt32(number2, 2);
            // Calculate the expected result.
            int expectedResult = 0;

            int counter = 0;
            bool returnOK = false;
            do
            {
                counter++;
                if (counter == 5)
                {
                    returnOK = true;
                }
                if (calcOperator.Equals("+"))
                {
                    expectedResult = value1FromTaskAsInt + value2FromTaskAsInt;
                }
                else
                {
                    expectedResult = value1FromTaskAsInt - value2FromTaskAsInt;
                }

                if (expectedResult > 15)
                {
                    value1FromTaskAsInt = value1FromTaskAsInt / 2;
                    value2FromTaskAsInt = value2FromTaskAsInt / 2;
                }

                if (expectedResult < 0)
                {
                    int temp = value1FromTaskAsInt;
                    value1FromTaskAsInt = value2FromTaskAsInt;
                    value2FromTaskAsInt = temp;
                }

                if (expectedResult < 15 && expectedResult > 0)
                {
                    returnOK = true;
                }
            } while (returnOK == false);

            number1 = value1FromTaskAsInt.ToString();
            number2 = value2FromTaskAsInt.ToString();
            if (counter < 5)
            {
                CCPUTask task = new CCPUTask();
                task.setNumber1(number1);
                task.setNumber2(number2);
                task.setCalcOperator(calcOperator);
                tasks.Add(task);
            }
        }
        //Gets an task for the CPU
        public CCPUTask getTask()
        {
            CCPUTask task = tasks[tasks.Count - 1];
            tasks.RemoveAt(tasks.Count - 1);
            return task;
        }
        //Returns the number of tasks
        public int getTaskLenght()
        {
            return tasks.Count;
        }
        void Start()
        {
            //Getting Gameobjects
            HDD = GameObject.Find("HDDConnector");
            CPUTick = GameObject.Find("CPUTick");
            material = CPUTick.GetComponent<CCPUTick>().getMaterials();
        }

        void Update()
        {
            Renderer rend = GetComponent<Renderer>();
            Tick = CPUTick.GetComponent<CCPUTick>().getTick();
            //If Tick is true then numbers will be send to the HDD
            if (Tick == false)
            {
                rend.sharedMaterial = material[0];
            }
            else
            {
                rend.sharedMaterial = material[1];
                if (KIOutput == true)
                {
                    CPUKi();
                }
                if (inputNumbers.Count > 1)
                {
                    createTask();
                }

                if (outputNumbers.Count > 1)
                {
                    sendNumber();
                }
            }


        }
    }
}
