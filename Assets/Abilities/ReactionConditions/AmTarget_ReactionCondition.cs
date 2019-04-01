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



    public AmTarget_ReactionCondition(ActionManager actionManager, Being parentBeing, string reactionConditionName) : base(actionManager, parentBeing, reactionConditionName)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.reactionConditionName = reactionConditionName;

    }
}
