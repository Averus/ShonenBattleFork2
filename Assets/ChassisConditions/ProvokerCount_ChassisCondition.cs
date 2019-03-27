using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvokerCount_ChassisCondition : ChassisCondition
{

    int param = -1;


    public override bool CanThisBeUsed()
    {
        if (param != -1)
        {
            if (actionManager.currentAction != null)
            {
                if (actionManager.currentAction.provoker.actors.Count == param)
                {
                    return true;
                }
                return false;
            }
            Debug.Log("Error: ActionManager current action is null");
            return false;
        }
        Debug.Log("Error: ProvokerCount ChassisCondition has no parameter!");
        return false;
    }

    public ProvokerCount_ChassisCondition(ActionManager actionManager, int param): base(actionManager)
    {
        this.actionManager = actionManager;
        this.param = param;
    }
}
