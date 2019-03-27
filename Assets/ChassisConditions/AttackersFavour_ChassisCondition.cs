using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackersFavour_ChassisCondition : ChassisCondition
{

    int favour = -1;

    public override bool CanThisBeUsed()
    {
        if (favour != -1)
        {
            if (actionManager.currentAction != null)
            {
                if (actionManager.attackersFavour == favour)
                {
                    //Debug.Log("AttackersFavour Condition reports TRUE: actual favour " + actionManager.attackersFavour + " looking for " + favour);
                    return true;
                }
                 //Debug.Log("AttackersFavour Condition reports FALSE: actual favour " + actionManager.attackersFavour + " looking for " + favour);
                return false;
            }
            Debug.Log("Error: ActionManager current action is null");
            return false;
        }
        Debug.Log("Error: AttackersFavour ChassisCondition has no parameter!");
        return false;


    }




    public AttackersFavour_ChassisCondition(ActionManager actionManager, int favour): base(actionManager)
    {
        this.actionManager = actionManager;
        this.favour = favour;
    }


}


