using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionType_ChassisCondition : ChassisCondition {

    public ThoughtType actiontype;



    public override bool CanThisBeUsed()
    {
            if (actionManager.currentAction.actionType == actiontype)
            {
                return true;
            }
        return false;
    }

    public  ActionType_ChassisCondition(ActionManager actionManager, ThoughtType actiontype): base(actionManager)
    {
        this.actionManager = actionManager;
        this.actiontype = actiontype;
    }
}
