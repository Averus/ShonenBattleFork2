using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAndProvokerAbilityHit_FunctionCall : FunctionCall
{
    private ActionManager actionManager;

    public override void Use()
    {
        actionManager.UseCurrentActionActorAndProvokerEffects(CombatState.Hit);
    }

    public ActorAndProvokerAbilityHit_FunctionCall(ActionManager actionManager)
    {
        this.actionManager = actionManager;
        //Debug.Log("ActorAndProvokerAbilityHit_FunctionCall created");
    }

}