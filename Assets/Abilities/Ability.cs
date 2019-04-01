using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum AbilityType
{
    Priority = 1, //currentl;y unused, might be for essential game related actions
    Instant = 100,
    PublicNormal = 200,
}

public enum AbilityChassis
{
    Melee = 100,
    Beam = 200,
    Wave = 300,
    Ball = 400,
    BigBall = 500,
    Blast = 600,

    Block = 700,
    Dodge = 800,
}

[System.Serializable]
public class Ability {

    //Abilities can be used as defences, although most are not, in order to be used as a defence isDefence must be set to true, and a defenceSpeed must be given.
    public bool isPassive = false;
    public bool isDefence = false;
    public int defenceSpeed = 0;
    ActionManager battlemanager;
    public string abilityName = "BLANK ABILITY";
    public int ranks = 0;
    public int numberOfTargets = 1;
    //New bits for the action manager
    public AbilityChassis abilityChassis;
    public AbilityType abilityType = AbilityType.PublicNormal;
    public float reflex = 0;
    public float abilitySpeed = 0;
    public string visual; //change later for a more robust visual package solution
    private Being parentBeing;

    public Being GetParentBeing()
    {
        if (parentBeing != null)
        {
            return parentBeing;
        }
        
        Debug.Log("ERROR: " + abilityName + " has no parent being set");
        return null;
    }

    public List<Condition> conditions = new List<Condition>();
    public List<Effect> effects = new List<Effect>();
    public List<Animation> animations = new List<Animation>();
    public List<TargetingCriteria> targetingCriteria = new List<TargetingCriteria>();
    public List<Being> validTargets = new List<Being>();


    private void checkForValidTargets(List<BeingToken> beings)
    {

        if (targetingCriteria.Count == 0)
        {
            Debug.Log("ERROR: " + abilityName + " has no Target list! (This shouldn't be the case!)");
            return;
        }
        if (beings.Count == 0)
        {
            Debug.Log("ERROR: " + abilityName + " was given an empty list to look in for possible targets!");
            return;
        }

        validTargets.Clear();// empty the list

        for (int i = 0; i < beings.Count; i++)                     //For each combatant in the fight...
        {
            for (int ii = 0; ii < targetingCriteria.Count; ii++)   //check them against each TargetingCriteria rule this ability has...
            {
                if (targetingCriteria[ii].CanThisBeTargeted(beings[i].being))     // Each Target rule can evaluate whether a given Being can be targeted...
                {
                    validTargets.Add(beings[i].being);                  //If even one of the Target rules returns 'true' then that combatant is added to the temporary list of validTargets, to be returned.

                }
            }
        }

    } //We might need a getValidTargets that returns a list for the active version of this ability

    /*
    public bool CanThisBeUsed()
    {

        checkForValidTargets(battlemanager.combatants); // It's a bit ugly, but I get combatants from battlemanager here to save passing battlemanager down the whole chain.

        if (validTargets.Count == 0)
        {
            Debug.Log(abilityName + " cannot be used, there are no valid targets");
            return false;
        }

        for (int i = 0; i < conditions.Count; i++)
        {
            if (!conditions[i].CanThisBeUsed()) //if one of the abilities conditions fails a check, return false
            {
                //Debug.Log(abilityName + " cannot be used");
                return false;
            }
        }

        //Debug.Log(abilityName + " can be used");
        return true; 
    }
    */

    public bool CanThisBeUsed(ActionManager actionManager) //this is the new version for Action manager
    {

        checkForValidTargets(actionManager.LIST1); // It's a bit ugly, but I get combatants from actionManager here. I do think actionmanager should always be visible (and static)

        if (validTargets.Count == 0)
        {
            Debug.Log(abilityName + " cannot be used, there are no valid targets");
            return false;
        }

        for (int i = 0; i < conditions.Count; i++)
        {
            if (!conditions[i].CanThisBeUsed()) //if one of the abilities conditions fails a check, return false
            {
                //Debug.Log(abilityName + " cannot be used");
                return false;
            }
        }

        //Debug.Log(abilityName + " can be used");
        return true;
    }

    public void Use(Being target) //This is the 'active' version where a target has been supplied
    {
        //Debug.Log("Using " + abilityName);
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].Use(target); //mostly adds statmodulations and other token to the Beings EffectToken (not a class yet) list
        }
    }

    /*
    public void Use() //This is the 'passive' version where it selects a random target from validtargets. Later this might have its own Ai instead for 'stupid' to 'smart' passive abilities.
    {
        //Debug.Log("Using " + abilityName);

        if (numberOfTargets > 0)
        {
            for (int i = 0; i < numberOfTargets; i++) //fire the number of times you can...
            {
                int r = Random.Range(0, validTargets.Count); //pick a random target from those that are valid

                if (effects.Count > 0)
                {
                    for (int ii = 0; ii < effects.Count; ii++) //for each effect in this abilitys effects list
                    {
                        if (validTargets.Count > 0)
                        {
                            effects[ii].Use(validTargets[r]); //use that effect on the random valid target
                        }
                        else
                        {
                            Debug.Log("ERROR: " + abilityName + " has no targets in it's valid targets list");
                        }
                    }
                }
                else
                {
                    Debug.Log("ERROR: " + abilityName + " has no effects in it's effects list");
                }
            }
        }
        else
        {
            Debug.Log("ERROR: " + abilityName + " has no targets in it's targets list");
        }


    }

    */


    public Ability(Being parentBeing, string name, AbilityChassis abilityChassis, int ranks, int numberOfTargets, bool isPassive)
    {
        this.parentBeing = parentBeing;
        this.abilityName = name;
        this.abilityChassis = abilityChassis;
        this.ranks = ranks;
        this.numberOfTargets = numberOfTargets;
        this.isPassive = isPassive;
    }

}
