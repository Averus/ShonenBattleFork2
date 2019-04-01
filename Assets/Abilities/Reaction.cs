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

    public void PrioritiseAbilities(List<Thought> thoughts)
    {


        //Debug.Log(" looking for abilities that fit the bill... ");
        //Debug.Log(" in a list that's length " + abil.Count);


        for (int i = 0; i < selectionCriteria.Count; i++)
        {
            for (int ii = 0; ii < thoughts.Count; ii++)
            {
                selectionCriteria[i].Assess(thoughts[ii]);
            }
        }

    }

    public Reaction(ActionManager actionManager, Being parentBeing, string reactionName)
    {
        this.actionManager = actionManager;
        this.parentBeing = parentBeing;
        this.reactionName = reactionName;

    }
}
