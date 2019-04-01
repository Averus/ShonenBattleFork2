using UnityEngine;
using System.Collections;
using System;

public class NoCondition_Condition : Condition
{
    public override bool CanThisBeUsed()
    {
        return true;
    }

    public NoCondition_Condition(ActionManager actionManager, Being parentBeing, string conditionName) : base(actionManager, parentBeing, conditionName) //This should grab the actionManager from the base constructor (Condition)
    {
       
    }
}
