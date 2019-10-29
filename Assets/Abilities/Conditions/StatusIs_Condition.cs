using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusIs_Condition : Condition
{
    Being.Status status;

    public override bool CanThisBeUsed()
    {
        if (parentBeing.status == status)
        {
            return true;
        }

        return false;
    }



    public StatusIs_Condition(ActionManager actionManager, Being parentBeing, string conditionName, Being.Status status) : base(actionManager, parentBeing, conditionName)
    {
        this.status = status;

    }


}
