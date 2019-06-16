using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatModulation : Effect {


    //This class will be created by 'damage' effects, 'buff to hit' effects etc, anything that modulates a stat. They will get put in a modulation array in each being and resolved in BODMAS order.

    
    public Stat targetStat;
    public string modifier;
    public int value;

    public override void Use(Being target)
    {
        //Debug.Log("using " + targetStat.statName + " token");

        if (modifier == "+")
        {
            //targetStat.current += value;
            return;
        }

        if (modifier == "-")
        {
            //targetStat.current -= value;
            return;
        }

        else
        {
            Debug.Log("ERROR: modifier requested was not recognised");
        } 

    }

    

    public StatModulation(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, CombatState usedInState, Stat targetStat, string modifier, int value, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, usedInState, persistsForRounds)
    {
        this.targetStat = targetStat;
        this.modifier = modifier;
        this.value = value;
    }
}
