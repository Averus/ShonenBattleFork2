using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyAction_DefenceEffect : Effect {

    public override void Use(Being target)
    {
        Debug.Log(target.beingName + ": Too slow!");

    }






    public AnyAction_DefenceEffect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, CombatState usedInState, int persistsForTurns, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, usedInState, persistsForTurns, persistsForRounds)
    {

    }
}
