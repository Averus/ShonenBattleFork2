using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction {

    public ActionManager actionManager;
    public Being parentBeing;


    public string reactionName = "BLANK REACTION";

    public List<ReactionCondition> reactionConditions = new List<ReactionCondition>(); //reaction conditions are specifically to do with reacting to an incoming ability
    public List<Condition> conditions = new List<Condition>();
    public List<SelectionCriteria> selectionCriteria = new List<SelectionCriteria>();
    public List<TargetingCriteria> targetingCriteria = new List<TargetingCriteria>();
    public ThoughtType outputThoughtType = ThoughtType.Normal;


    public bool CanThisBeDone(Action action)
    {
        for (int i = 0; i < reactionConditions.Count; i++)
        {
            if (!reactionConditions[i].CanThisBeUsed(action))
            {
                //Debug.Log(behaviourName + " cannot be done"); //should maybe say something like "beingname chooses not to behaviourname
                return false;
            }
        }

        for (int i = 0; i < conditions.Count; i++)
        {
            if (!conditions[i].CanThisBeUsed())
            {
                //Debug.Log(behaviourName + " cannot be done"); //should maybe say something like "beingname chooses not to behaviourname
                return false;
            }
        }

        //Debug.Log(behaviourName + " can be done");
        return true;
    }

    private bool IsThisAbilitySuitable(Ability ability)
    {
        if (selectionCriteria == null)
        {
            Debug.Log("Error: Reaction " + reactionName + " in Being " + parentBeing.beingName + " has no selection criteria");
            return false;
        }

        for (int i = 0; i < selectionCriteria.Count; i++)
        {
            if (!selectionCriteria[i].IsThisSuitable(ability))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsThisTargetSuitable(Being candidate)
    {
        for (int i = 0; i < targetingCriteria.Count; i++)
        {
            if (!targetingCriteria[i].CanThisBeTargeted(candidate))
            {
                return false;
            }
        }
        return true;
    }

    private List<Being> GetAcceptableTargets(List<Being> candidates)
    {
        if (targetingCriteria == null)
        {
            Debug.Log("Error: Reaction " + reactionName + " in Being " + parentBeing.beingName + " has no targeting criteria");
            return null;
        }

        List<Being> validTargets = new List<Being>();

        for (int i = 0; i < candidates.Count; i++)
        {
            if (IsThisTargetSuitable(candidates[i]))
            {
                validTargets.Add(candidates[i]);
            }
        }

        return validTargets;
    }

    public List<Thought> GenerateThoughts(List<Ability> useableAbilities)
    {
        List<Thought> thoughts = new List<Thought>();

        for (int i = 0; i < useableAbilities.Count; i++)
        {
            if (IsThisAbilitySuitable(useableAbilities[i]))
            {
                List<Being> acceptableTargets = GetAcceptableTargets(useableAbilities[i].validTargets);
                List<Being> chosenTargets = new List<Being>();

                //Here we need to add functionality to select targets for abilities that can target the same being multiple times vs only once per being

                for (int ii = 0; ii < useableAbilities[i].numberOfTargets; ii++)
                {
                    chosenTargets.Add(acceptableTargets[Random.Range(0, acceptableTargets.Count)]);
                }

                thoughts.Add(new Thought(outputThoughtType, 0, useableAbilities[i], chosenTargets));
            }
        }
        return thoughts;
    }

    public Reaction(ActionManager actionManager, Being parentBeing, string reactionName, ThoughtType outputThoughtType)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.reactionName = reactionName;
        this.outputThoughtType = outputThoughtType;

    }
}
