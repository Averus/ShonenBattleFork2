using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge_DefenceEffect : Effect
{
    public override void Use(Being target)
    {
        Debug.Log(target.beingName + " flickers as they dodge with super speed.");

        for (int i = actionManager.effectsInPlay.Count - 1; i > -1; i--)
        {
            //This is farly simplistic and may need to be revisited if the effectsInPlay list becomes full of items from other turns etc
            if (actionManager.effectsInPlay[i].effect.UsedInState == CombatState.Hit)
            {
                actionManager.effectsInPlay.RemoveAt(i);
            }
        }

    }






    public Dodge_DefenceEffect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, CombatState usedInState, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, usedInState, persistsForRounds)
    {

    }


}
