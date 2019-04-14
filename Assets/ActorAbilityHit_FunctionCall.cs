using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActorAbilityHit_FunctionCall : FunctionCall
{
    private ActionManager actionManager;

    public override void Use()
    {
        actionManager.UseCurrentActionActorEffects(CombatState.Hit);
    }

    public ActorAbilityHit_FunctionCall(ActionManager actionManager)
    {
        this.actionManager = actionManager;
        //Debug.Log("ActorAbilityHit_FunctionCall created");
    }
}