using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusIsNot_Condition : Condition {

    Being.Status status;

    public override bool CanThisBeUsed()
    {
        if (parentBeing.status != status)
        {
            return true;
        }

        return false;
    }



    public StatusIsNot_Condition(ActionManager actionManager, Being parentBeing, string conditionName, Being.Status status) : base(actionManager, parentBeing, conditionName)
    {
        this.status = status;

    }
}
