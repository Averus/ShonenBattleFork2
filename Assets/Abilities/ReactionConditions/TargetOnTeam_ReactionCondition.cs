using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOnTeam_ReactionCondition : ReactionCondition
{
    int team;

    public override bool CanThisBeUsed(Action action)
    {
        for (int i = 0; i < action.targets.Count; i++)
        {
            if (action.targets[i].team == team)
            {
                return true;
            }
        }

        return false;
    }


    public TargetOnTeam_ReactionCondition(ActionManager actionManager, Being parentBeing, string reactionConditionName, int team) : base(actionManager, parentBeing, reactionConditionName)
    {
        this.team = team;

    }
}
