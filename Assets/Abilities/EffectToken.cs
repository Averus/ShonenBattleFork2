using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EffectToken
{
    string effectTokenName = "NO EFFECT TOKEN NAME";

    public abstract void Use();

}
