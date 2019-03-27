using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorChassis_ChassisCondition : ChassisCondition
{


    public AbilityChassis chassis;

public override bool CanThisBeUsed()
{
    if (actionManager.currentAction.ability.abilityChassis == chassis)
    {
        return true;
    }
    return false;
}



public ActorChassis_ChassisCondition(ActionManager actionManager, AbilityChassis chassis): base(actionManager)
    {
    this.actionManager = actionManager;
    this.chassis = chassis;
}
}
