using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainsEffect_ReactionCondition : ReactionCondition
{


    String effectName;


    public override bool CanThisBeUsed(Action action)
    {
        
        for (int i = 0; i < action.ability.effects.Count; i++)
        {
            if (action.ability.effects[i].effectName == effectName)
            {
                return true;
            }
        }

        return false;

    }







    public ContainsEffect_ReactionCondition(BattleManager battleManager, Being parentBeing, string reactionConditionName, string effectName) : base(battleManager, parentBeing, reactionConditionName)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.reactionConditionName = reactionConditionName;
        this.effectName = effectName;

    }
}
