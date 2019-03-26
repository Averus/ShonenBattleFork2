using UnityEngine;
using System.Collections;

public abstract class SelectionCriteria {


    public BattleManager battleManager;
    public Being parentBeing;
    public Ability parentAbility;
    public string selectionCriteriaName;

    public float minPriority;
    public float maxPriority;


    public abstract void Assess(Thought thought);


    public SelectionCriteria(BattleManager battleManager, Being parentBeing, string selectionCriteriaName, float minPriority, float maxPriority)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.selectionCriteriaName = selectionCriteriaName;
        this.minPriority = minPriority;
        this.maxPriority = maxPriority;
    }

}
