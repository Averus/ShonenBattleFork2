using UnityEngine;
using System.Collections;
using System;

public class Damage_Effect : Effect
{

    public string targetResource;
    public int baseDamage;


    public override void Use(Being target)
    {
        float damageRoll = 0;

        for (int i = 0; i < baseDamage; i++)
        {
            //roll 1d6
            damageRoll += UnityEngine.Random.Range(1, 6);
        }
        
        float plModulation = actionManager.GetPowerLevelComparison(parentBeing, target);
        
        float finalDamage = 0 - (damageRoll * plModulation); //0- makes it negative because it's damage
        

        Debug.Log("base damage: " + baseDamage + " damage roll: " + damageRoll + " parentPL " + parentBeing.GetResourceValue("POWERLEVEL",1) + " targetpl " + target.GetResourceValue("POWERLEVEL",1) + " plmodulation: " + plModulation + " final damage" + finalDamage);

        Resource targetResourceInTargetBeing;
        targetResourceInTargetBeing = target.GetResource(targetResource);
        ResourceModifier rm = new ResourceModifier(finalDamage);
        targetResourceInTargetBeing.Modify(rm);
        


    }


    public Damage_Effect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, string targetResource, int damage, CombatState usedInState, int persistsForTurns, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, usedInState, persistsForTurns, persistsForRounds)
    {
        this.baseDamage = damage;
        this.targetResource = targetResource;
    } 


}
