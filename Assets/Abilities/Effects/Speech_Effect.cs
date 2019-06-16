using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech_Effect : Effect {


    string speech = "NO SPEECH IMPLEMENTED";

    public override void Use(Being target)
    {
        Debug.Log(speech);
    }


    public Speech_Effect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, CombatState usedInState, string speech, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, usedInState, persistsForRounds)
    {
        this.speech = speech;
    }

}
