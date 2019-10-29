using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBeingsTurn_Condition : Condition
{
    Being targetBeing;

    public override bool CanThisBeUsed()
    {
        if (actionManager.LIST1[actionManager.currentTurn].being == targetBeing)
        {
            return true;
        }

        return false;
    }

    public IsBeingsTurn_Condition(ActionManager actionManager, Being parentBeing, string conditionName, Being targetBeing) : base(actionManager, parentBeing, conditionName)
    {
        this.targetBeing = targetBeing;
    }
}
