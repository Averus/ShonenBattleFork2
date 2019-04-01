using UnityEngine;
using System.Collections;

public abstract class Condition {

    //this is an abstract class beacuse they can use constructors and interfaces cannot. The constructor is used to pass the ActionManager reference

    public ActionManager actionManager;
    public Being parentBeing;
    public Ability parentAbility;

    string conditionName;

    public string GetConditionName()
    {
        return conditionName;
    }


    public abstract bool CanThisBeUsed();

 

    public Condition(ActionManager actionManager, Being parentBeing, string conditionName)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.conditionName = conditionName;

    }

}

