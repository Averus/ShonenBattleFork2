using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTeam_TargetingCriteria : TargetingCriteria
{
    int team;

    public override bool CanThisBeTargeted(Being potentialTarget)
    {
        if (potentialTarget.team == team)
        {
            return true;
        }

        return false;
    }

    public OnTeam_TargetingCriteria(ActionManager actionManager, Being parentBeing, int team) : base(actionManager, parentBeing)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.team = team;
    }
}
