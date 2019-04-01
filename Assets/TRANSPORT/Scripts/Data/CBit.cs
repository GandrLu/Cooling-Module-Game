
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISIS.transport
{
    public class CBit : MonoBehaviour
    {
        //Materials for the bits
        public Material[] materials;
        private Renderer rend;
        //Bitstate of the bit
        public enum bitState
        {
            empty,
            zero,
            one
        }
        //Bitstate is empty as default
        public bitState bit = bitState.empty;
        //Gets the string of the bit
        public string getString()
        {
            switch (bit)
            {
                case bitState.zero:
                    return "0";
                    break;
                case bitState.one:
                    return "1";
                    break;
                default:
                    return "2";
            }
        }
        //Sets the string of an bit
        public void setString(string _value)
        {
            if (_value.Equals(0))
            {
                bit = bitState.zero;
            }
            else
            {
                bit = bitState.one;
            }
            updateMaterial();
        }
        //Sets an bit with an bitstate
        public void setBit(bitState state)
        {
            bit = state;
            if (!rend) rend = GetComponent<Renderer>();
            updateMaterial();
        }
        //Material will be updated
        void updateMaterial()
        {
            // Debug.Log(string.Format("updateMaterial {1} {0} {2}", this.bit, this.gameObject.name, rend.gameObject.name));
            switch (this.bit)
            {
                case bitState.zero:
                    if (materials[1])
                        rend.material = materials[1];
                    break;
                case bitState.one:
                    if (materials[2])
                        rend.material = materials[2];
                    break;
                default:
                    if (materials[0])
                        rend.material = materials[0];
                    break;
            }
        }
        void Start()
        {
            //rend = GetComponent<Renderer>();
            //updateMaterial();
        }
        void Update()
        {
            // Renderer rend = GetComponent<Renderer>();
            // if (this.bit == bitState.one)
            // {
            // rend.material.shader = Shader.Find("_Color");
            // rend.material.SetColor("_Color", Color.black);
            // }
            // else
            // {
            // rend.material.shader = Shader.Find("_Color");
            // rend.material.SetColor("_Color", Color.white);
            // }
        }

    }
}