using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReactionCondition {

    public ActionManager actionManager;
    public Being parentBeing;
    public Ability parentAbility;
    public string reactionConditionName;


    public abstract bool CanThisBeUsed(Action action);



    public ReactionCondition(ActionManager actionManager, Being parentBeing, string reactionConditionName)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.reactionConditionName = reactionConditionName;

    }
}
