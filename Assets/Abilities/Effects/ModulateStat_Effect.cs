using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulateStat_Effect : Effect
{

    public string targetStat;
    StatModType modifierType;
    public int value;
    public CombatState usedInState;


    public override void Use(Being target)
    {
        Stat actualTargetStat = target.GetStat(targetStat);

        if (actualTargetStat == null)
        {
            Debug.Log(target.beingName + " has no " + targetStat + "!");
            return;
        }

        StatModifier sm = new StatModifier(value, modifierType);
        actualTargetStat.AddModifier(sm);
        
    }



    public ModulateStat_Effect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, string targetStat, StatModType modifierType, int value, CombatState usedInState, int persistsForTurns, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, usedInState, persistsForTurns, persistsForRounds)
    {
        this.targetStat = targetStat;
        this.modifierType = modifierType;
        this.value = value;
        this.usedInState = usedInState;
    }
}

