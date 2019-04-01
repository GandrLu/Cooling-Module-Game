
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBit : MonoBehaviour {

    public bool[] m_data; 
    public GameObject[] bits;

    void Start()
    {
        bits = GameObject.FindGameObjectsWithTag("Bits");
    }
    void Update()
    {
        for (int i = 0; i < bits.Length; i++)
        {
            Renderer rend = bits[i].GetComponent<Renderer>();
            if (m_data[i])
            {
                rend.material.SetColor("_Color", Color.black);
            }
            else
            {
                rend.material.SetColor("_Color", Color.white);
            }
            
        }
    }
    public void setData(bool[] _data)
    {
        this.m_data = _data;
    }

    public bool[] getData()
    {
        return this.m_data;
    }
}
