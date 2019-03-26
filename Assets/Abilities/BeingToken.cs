using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BeingToken{

    public float reflexSpeed;
    public Being being;

    public BeingToken(Being being, float reflexSpeed)
    {
        this.being = being;
        this.reflexSpeed = reflexSpeed;
    }
}
