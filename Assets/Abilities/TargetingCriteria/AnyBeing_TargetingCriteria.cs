using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyBeing_TargetingCriteria : TargetingCriteria
{


    public override bool CanThisBeTargeted(Being potentialTarget)
    {
        return true;
    }

    public AnyBeing_TargetingCriteria(ActionManager actionManager, Being parentBeing) : base(actionManager, parentBeing)
    {
    }
}
