using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectToken
{
    public Being target;

    public Effect effect;

    public EffectToken(Effect effect, Being target)
    {
        this.target = target;
        this.effect = effect;
    }

}
