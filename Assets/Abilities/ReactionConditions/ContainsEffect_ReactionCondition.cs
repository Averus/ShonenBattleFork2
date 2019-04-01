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







    public ContainsEffect_ReactionCondition(ActionManager actionManager, Being parentBeing, string reactionConditionName, string effectName) : base(actionManager, parentBeing, reactionConditionName)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.reactionConditionName = reactionConditionName;
        this.effectName = effectName;

    }
}
