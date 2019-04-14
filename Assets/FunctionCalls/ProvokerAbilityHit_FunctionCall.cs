using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvokerAbilityHit_FunctionCall : FunctionCall
{
    private ActionManager actionManager;

    public override void Use()
    {
        actionManager.UseCurrentActionProvokerEffects(CombatState.Hit);
    }

    public ProvokerAbilityHit_FunctionCall(ActionManager actionManager)
    {
        this.actionManager = actionManager;
        //Debug.Log("ProvokerAbilityHit_FunctionCall created");
    }
}