using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvokerChassis_ChassisCondition : ChassisCondition
{


    public AbilityChassis chassis;

public override bool CanThisBeUsed()
{
    if (actionManager.currentAction.provoker.ability.abilityChassis == chassis)
    {
        return true;
    }

        //Debug.Log("Provoker chassis reports FALSE: " + actionManager.currentAction.provoker.ability.abilityChassis + " looking for " + chassis);
    return false;
}



public ProvokerChassis_ChassisCondition(ActionManager actionManager, AbilityChassis chassis): base(actionManager)
    {
    this.actionManager = actionManager;
    this.chassis = chassis;
}
}
