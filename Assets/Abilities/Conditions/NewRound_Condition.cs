using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRound_Condition : Condition
{


    public override bool CanThisBeUsed()
    {
        throw new System.NotImplementedException();
    }

    public NewRound_Condition(ActionManager actionManager, Being parentBeing, string conditionName) : base(actionManager, parentBeing, conditionName)
    {
    }
}
