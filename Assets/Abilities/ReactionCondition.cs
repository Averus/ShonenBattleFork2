using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReactionCondition {

    public BattleManager battleManager;
    public Being parentBeing;
    public Ability parentAbility;
    public string reactionConditionName;


    public abstract bool CanThisBeUsed(Action action);



    public ReactionCondition(BattleManager battleManager, Being parentBeing, string reactionConditionName)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.reactionConditionName = reactionConditionName;

    }
}
