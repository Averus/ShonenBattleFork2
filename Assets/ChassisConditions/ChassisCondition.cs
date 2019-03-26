using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChassisCondition {

    public ActionManager actionManager;

    public abstract bool CanThisBeUsed();

    public ChassisCondition(ActionManager actionManager)
    {
        this.actionManager = actionManager;
    }
}
