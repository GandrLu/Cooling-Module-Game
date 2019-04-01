
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace AISIS.transport
{
    public class CNibble : MonoBehaviour
    {

        // A Container for 4 Bits
        // https://de.wikipedia.org/wiki/Nibble
        private CBit[] bits = new CBit[4];  // Index 0 = LSB (Lowest Signifcant Bit), index 3 = MSB
        //Destination of the 4 bits
        public string destination = "";
        //Value of the bits as number
        public int value = -1;
        //Standard constructor
        public CNibble()
        {
        }
        //Constructor with integer
        public CNibble(int _value)
        {
            this.value = _value;
        }
        //Constructor with strings
        public CNibble(string _value)
        {
            // for (int i = 0; i < 3; i++)
            // {
            //     this.bits[i].setString(_value[i].ToString());
            // }
            this.value = System.Convert.ToInt32(_value,2);
        }
        //Returns alls bits
        public CBit getBit(int _index)
        {
            return bits[_index];
        }
        //Sets an bit to the value
        public void setBit(int _index, CBit bit)
        {
            bits[_index] = bit;
        }
        //set all bits
        public void setAllBits(CBit[] _bits)
        {
            int num = 0;
            foreach (CBit cBit in _bits)
            {

                setBit(num, cBit);
                num++;
            }
        }

        public void setBitValues() {
            // Debug.Log(string.Format("CNibble CBit.length: {0} Value: {1} {2}", this.bits.Length, value, this.bits[0].getString()));
            for (int i = 0; i < 4; i++)
            {
                if (value < 0)
                {
                    this.bits[i].setBit(CBit.bitState.empty);
                }
                else
                {
                    this.bits[i].setBit(((value & (1 << i)) != 0) ? CBit.bitState.one : CBit.bitState.zero); // return (b & (1 << pos)) != 0;
                }
            }
            TextMeshPro tmp = transform.Find("TextValue").GetComponent<TextMeshPro>();
            tmp.text = value.ToString();
        }
        //Set all bits with an string
        public void setString(string _value)
        {
            bits[0].setString(_value[0].ToString());
            bits[1].setString(_value[1].ToString());
            bits[2].setString(_value[2].ToString());
            bits[3].setString(_value[3].ToString());
        }
        //Gets all bits as an string
        public string getString()
        {
            string returnString = "";
            returnString += bits[3].getString();    // MSB as first character in string
            returnString += bits[2].getString();
            returnString += bits[1].getString();
            returnString += bits[0].getString();
            return returnString;
        }
        //Sets destination of the nibble
        public void setDestination(string _value)
        {
            this.destination = _value;
        }
        //Returns the destination of the nibble
        public string getDestination()
        {
            return this.destination;
        }

        void Start()
        {
            // List<GameObject> children = new List<GameObject>();
            int i = 0;
            foreach (Transform child in transform)
            {
                if (child.GetComponent<CBit>()) {
                    bits[i++] = child.GetComponent<CBit>();
                    if (i > 3) break;
                };
            }
            setBitValues();
        }
        void Update()
        {

        }

    }
}
