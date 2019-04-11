using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectToken
{
    public List<Being> targets;

    public Effect effect;

    public EffectToken(Effect effect, List<Being> targets)
    {
        this.targets = targets;
        this.effect = effect;
    }

}
