using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicResolutionChecks_FunctionCall : FunctionCall
{
    RuleFunctions ruleFunctions;
    Being parentBeing;

    public override void Use()
    {
        ruleFunctions.BasicResolutionChecks(parentBeing);
    }

    public BasicResolutionChecks_FunctionCall(RuleFunctions ruleFunctions, Being parentBeing)
    {
        this.ruleFunctions = ruleFunctions;
        this.parentBeing = parentBeing;

    }
}
