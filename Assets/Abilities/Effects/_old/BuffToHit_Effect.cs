using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffToHit_Effect : Effect {

    int buff;
    string modulator;



    public override void Use(Being target)
    {
        //

    }


    public BuffToHit_Effect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, string modulator, int buff, CombatState usedInState, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, usedInState, persistsForRounds)
    {
        this.buff = buff;
        this.modulator = modulator;
    }
}
