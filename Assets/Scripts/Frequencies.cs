using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frequencies
{
    public int index;
    public float data;
    public float dataNormalized;

    
    public void AddData(float data)
    {
        this.data += data;
    }
}
