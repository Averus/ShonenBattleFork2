using UnityEngine;
using System.Collections;

public abstract class SelectionCriteria {


    public ActionManager actionManager;
    public Being parentBeing;
    public Ability parentAbility;
    public string selectionCriteriaName;


    public abstract bool IsThisSuitable(Ability ability);


    public SelectionCriteria(ActionManager actionManager, Being parentBeing, string selectionCriteriaName)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.selectionCriteriaName = selectionCriteriaName;
    }

}
