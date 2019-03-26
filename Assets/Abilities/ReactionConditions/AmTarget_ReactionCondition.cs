using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmTarget_ReactionCondition : ReactionCondition
{


    public override bool CanThisBeUsed(Action action)
    {
        for (int i = 0; i < action.targets.Count; i++)
        {
            if (action.targets[i] == parentBeing)
            {
                return true;
            }
        }

        return false;
    }



    public AmTarget_ReactionCondition(BattleManager battleManager, Being parentBeing, string reactionConditionName) : base(battleManager, parentBeing, reactionConditionName)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.reactionConditionName = reactionConditionName;

    }
}
