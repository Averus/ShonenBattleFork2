using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentActionActor_TargetingCriteria : TargetingCriteria
{

    public override bool CanThisBeTargeted(Being potentialTarget)
    {
        for (int i = 0; i < actionManager.currentAction.actors.Count; i++)
        {
            if (actionManager.currentAction.actors[i] == potentialTarget)
            {
                return true;
            }
        }

        return false;
    }


    public CurrentActionActor_TargetingCriteria(ActionManager actionManager, Being parentBeing) : base(actionManager, parentBeing)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
    }
    
}
