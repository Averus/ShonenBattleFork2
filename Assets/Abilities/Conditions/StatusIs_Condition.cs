using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusIs_Condition : Condition
{
    Being.Status status;

    public override bool CanThisBeUsed()
    {
        throw new System.NotImplementedException();
    }



    public StatusIs_Condition(ActionManager actionManager, Being parentBeing, string conditionName, Being.Status status) : base(actionManager, parentBeing, conditionName)
    {
        this.status = status;

    }


}
