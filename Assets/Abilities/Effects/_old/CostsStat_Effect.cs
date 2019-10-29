using UnityEngine;
using System.Collections;
using System;

public class CostsStat_Effect : Effect {

    int cost;
    String statName;

    public override void Use(Being target)
    {
        //Debug.Log("using stamina cost effect");

        Stat s = parentBeing.GetStat(statName);

        if (s!= null)
        {

            //StatModulation sm = new StatModulation(s, "-", cost);
            //actionManager.effectQueue.Add(sm);
            //Debug.Log( parentBeing.beingName + "gets a " + statName + " -" + cost + " token");
            return;
        }

        Debug.Log("ERROR: " + parentBeing.beingName + " does not have a " + statName + " stat.");
        



    }

    public CostsStat_Effect(ActionManager actionManager, Being parentBeing, Ability parentAbility, String effectName, String statName, int cost, CombatState usedInState, int persistsForTurns, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, usedInState, persistsForTurns, persistsForRounds)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;
        this.statName = statName;
        this.cost = cost;
    }
}
