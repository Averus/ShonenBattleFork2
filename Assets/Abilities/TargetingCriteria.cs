using UnityEngine;
using System.Collections;

public abstract class TargetingCriteria{

    //this is an abstract class beacuse they can use constructors and interfaces cannot. The constructor is used to pass the ActionManager reference

    public ActionManager actionManager;
    public Being parentBeing;
    public Ability parentAbility;


    public abstract bool CanThisBeTargeted(Being potentialTarget);



    public TargetingCriteria(ActionManager actionManager, Being parentBeing, Ability parentAbility)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;

    }
}
