using UnityEngine;
using System.Collections;
using System;

public class Self_TargetingCriteria : TargetingCriteria {



    public override bool CanThisBeTargeted(Being potentialTarget)
    {
        if (potentialTarget == parentBeing)
        {
            return true;
        }

        return false;
    }




    public Self_TargetingCriteria(ActionManager actionManager, Being parentBeing) : base(actionManager, parentBeing)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;

    }


}
