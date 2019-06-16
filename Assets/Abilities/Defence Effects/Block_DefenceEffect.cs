using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_DefenceEffect : Effect {

    public override void Use(Being target)
    {
        Debug.Log(target.beingName + " crosses their arms and blocks the hit.");

        //Look through effects in play
        for (int i = actionManager.effectsInPlay.Count - 1; i > -1; i--)
        {
           //find ModulateResource_Effects
            if (actionManager.effectsInPlay[i].effect is ModulateResource_Effect)
            {
                //Debug.Log(actionManager.effectsInPlay[i].effect.effectName + " in effectsInPlay IS a ModulateResourceEffect");

                //find 'Damage' effects
                if (actionManager.effectsInPlay[i].effect.effectName == "Damage")
                {
                    //cast the effect into a ModulateResource_Effect so its feilds can be accessed
                    ModulateResource_Effect mo = (ModulateResource_Effect)actionManager.effectsInPlay[i].effect;

                    //halve the damage
                    int damageAfterBlocking = mo.value / 2;

                    //create a new modulateResource_Effect to replace it - now with the halved damage
                    ModulateResource_Effect mod = new ModulateResource_Effect(actionManager, mo.parentBeing, mo.parentAbility, mo.effectName, mo.targetResource, damageAfterBlocking, mo.targetSelf, mo.UsedInState, mo.persistsForRounds);

                    //swap the effect in this effectToken for the new effect with halved damage
                    actionManager.effectsInPlay[i].effect = mod;
                }
            }

            
        }


    }



    public Block_DefenceEffect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, CombatState usedInState, int persistsForRounds) : base(actionManager, parentBeing, parentAbility, effectName, usedInState, persistsForRounds)
    {

    }
}

