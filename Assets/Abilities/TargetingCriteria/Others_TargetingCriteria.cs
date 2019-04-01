﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Others_TargetingCriteria : TargetingCriteria {


    public override bool CanThisBeTargeted(Being potentialTarget)
    {
        if (potentialTarget == parentBeing)
        {
            return false;
        }

        return true;
    }


    public Others_TargetingCriteria(ActionManager actionManager, Being parentBeing, Ability parentAbility) : base(actionManager, parentBeing, parentAbility)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;

    }

}
