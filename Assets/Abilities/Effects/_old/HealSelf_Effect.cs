using UnityEngine;
using System.Collections;
using System;

public class HealSelf_Effect : Effect {

    public int healByValue;

    public override void Use(Being target)
    {
        Stat hp = parentBeing.GetStat("HP");
        //hp.current += healByValue;
        Debug.Log(parentBeing.beingName + " heals itself for " + healByValue + " hp.");
        Debug.Log(parentBeing.beingName + " HP at " + hp.current);
    }


    public HealSelf_Effect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, int healAmount, CombatState usedInState, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, usedInState, persistsForRounds)
    {
        healByValue = healAmount;
    }

}
