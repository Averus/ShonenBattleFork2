using UnityEngine;
using System.Collections;

public class DamageSelf_Effect : Effect {

    int damage;



    public override void Use(Being target)
    {

        Stat hp = parentBeing.GetStat("HP"); 
        hp.current -= damage;
        Debug.Log(parentBeing.beingName + " hurts itself for " + damage + " damage.");
        Debug.Log(parentBeing.beingName + " HP at " + hp.current);

    }


    public DamageSelf_Effect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, int damage) : base(actionManager, parentBeing, parentAbility, effectName)
    {
        this.damage = damage;
    }
}
