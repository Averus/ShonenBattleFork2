using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Behaviour{

    public BattleManager battleManager;
    public Being parentBeing;


    public string behaviourName = "BLANK BEHAVIOUR";

    public List<Condition> conditions = new List<Condition>();
    public List<SelectionCriteria> selectionCriteria = new List<SelectionCriteria>();


    public bool CanThisBeDone()
    {
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

    public Behaviour(BattleManager battleManager, Being parentBeing, string behaviourName)
    {
        this.battleManager = battleManager;
        this.parentBeing = parentBeing;
        this.behaviourName = behaviourName;

    }

}
