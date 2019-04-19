using UnityEngine;
using System.Collections;
using System;

public class IncludesEffect_SelectionCriteria : SelectionCriteria
{

    String effectName;


    public override bool IsThisSuitable(Ability ability)
    {
        for (int i = 0; i < ability.effects.Count; i++)
        {
            if (ability.effects[i].effectName == effectName)
            {
                return true;
            }
        }

        return false;
    }





    public IncludesEffect_SelectionCriteria(ActionManager actionManager, Being parentBeing, string selectionCriteriaName, string effectName) : base(actionManager, parentBeing, selectionCriteriaName)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.effectName = effectName;
    }


}
