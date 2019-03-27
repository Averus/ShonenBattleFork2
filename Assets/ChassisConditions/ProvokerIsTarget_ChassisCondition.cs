using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvokerIsSoleTarget_ChassisCondition : ChassisCondition
{

    public override bool CanThisBeUsed()
    {
        if (actionManager.currentAction != null)
        {
            if (actionManager.currentAction.provoker != null)
            {
                if (actionManager.currentAction.targets[0] == actionManager.currentAction.provoker.actors[0])
                {
                    return true;
                }
            }
        }
        return false;
    }

    public ProvokerIsSoleTarget_ChassisCondition(ActionManager actionManager): base(actionManager)
    {
        this.actionManager = actionManager;
    }
}
