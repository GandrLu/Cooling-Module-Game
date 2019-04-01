using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISIS.transport
{

    public class CCPUTask
    {
        public string number1;
        public string number2;
        public string calcOperator;

        public CCPUTask()
        {

        }
        //Get number 1 of the task
        public string getNumber1()
        {
            return number1;
        }
        //Set number 1 of the task
        public void setNumber1(string _value)
        {
            this.number1 = _value;
        }
        //Get number 2 of the task
        public string getNumber2()
        {
            return number2;
        }
        //Set number 2 of the task
        public void setNumber2(string _value)
        {
            this.number2 = _value;
        }
        //Getting the calculation operator
        public string getCalcOperator()
        {
            return calcOperator;
        }
        //Setting the calculation operator
        public void setCalcOperator(string _value)
        {
            this.calcOperator = _value;
        }
    }
}
    
