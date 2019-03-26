using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActorCount_ChassisCondition : ChassisCondition
{

    int param = -1;

    public override bool CanThisBeUsed()
    {
        if (param != -1)
        {
            if (actionManager.currentAction != null)
            {
                if (actionManager.currentAction.actors.Count == param)
                {
                    return true;
                }
                return false;
            }
            Debug.Log("Error: ActionManager current action is null");
            return false;
        }
        Debug.Log("Error: ActorCount ChassisCondition has no parameter!");
        return false;


    }

    public ActorCount_ChassisCondition(ActionManager actionManager, int param): base(actionManager)
    {
        this.actionManager = actionManager;
        this.param = param;
    }
}
