using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Effect{

    public ActionManager actionManager;
    public Being parentBeing;
    public Ability parentAbility;

    public string effectName;

    public CombatState UsedInState;

    public abstract void Use(Being target);


    public Effect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, CombatState UsedInState)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.parentAbility = parentAbility;
        this.effectName = effectName;
        this.UsedInState = UsedInState;
    }
}
