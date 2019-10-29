using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeFocus_Effect : Effect
{

    public RuleFunctions ruleFunctions;

    public override void Use(Being target)
    {
        if (parentBeing.playerControlled == true)
        {
            actionManager.ruleFunctions.PlayerAttributesFocus(actionManager,actionManager.canvas.transform,target);
        }
        if (parentBeing.playerControlled == false)
        {
            Debug.Log("BEING CHOOSES TO FOCUS");
        }
    }


    public AttributeFocus_Effect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, CombatState UsedInState, int persistsForTurns, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, UsedInState, persistsForTurns, persistsForRounds)
    {

    }
}
