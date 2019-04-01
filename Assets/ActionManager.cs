﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionManager : MonoBehaviour {

    public VisualManager visualManager;
    public ChassisTable chassisTable;
    public bool turnNotFinishedYet = false;

    public List<BeingToken> LIST1 = new List<BeingToken>(); //A list of beings acting in the location
    public List<Thought> LIST2 = new List<Thought>();//A list of actions declared by said beings ready to be ordered and added to...
    public List<Action> LIST3 = new List<Action>(); //The main stack of public actions, ready to be resolved one by one

    public int currentTurn = 0;
    public int step = 0;
    public bool pause = false;

    public List<Effect> effectQueue = new List<Effect>(); //rename to effectsInPlay

    public Thought currentThought = null; //The current Thought that is being evaluated
    public Action currentAction = null; //The current action being evaluated
    public float attackersFavour = 0;

    // Use this for initialization
    void Start () {

        if (chassisTable == null)
        {
            Debug.Log("Error: Action Manager does not have a reference to Chassis Table");
            
        }
        else
        {
            chassisTable.actionManager = this;
            chassisTable.LoadChassisRulesCSV(); //Loads the Chassis rules 

            visualManager.actionManager = this;
            visualManager.LoadDefaultChassisVisualsCSV(); //loads the default chassis visuals, visuals from the start of an action
            visualManager.LoadResolutionVisualsCSV(); //loads the resolution visuals, visuals for the resolution of an action - what actually happens as a result 
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A)) //just to test things
        {
            //SortList1();
        }

        if (Input.GetMouseButtonDown(0) && pause == false)
        {
            if (step == 4)
            {
                NewRound();
                pause = true;
            }

            if (step == 3)
            {
                NextTurn();
                pause = true;
            }

            if (step == 2)
            {
                TakeAction();
                pause = true;
            }

            if (step == 1)
            {
                CommitToAction();
                pause = true;
            }

            if (step == 0)
           {
               step = 1; //fires the turn of the current Being, asking them to Think() and filling LIST2
               Step1();
               pause = true;
            }




        }

        if (Input.GetMouseButtonUp(0) && pause == true)
        {
            pause = false;
        }

    }

    public void AddToBeingQueue(BeingToken being)
    {
        LIST1.Add(being);
        SortList1();
    }

    void Step1()
    {
        turnNotFinishedYet = true;
        if (LIST1.Count > 0)
        {
            //ask Being to Think of some abilities to use
            List<Thought> tempList = LIST1[currentTurn].being.Think();

            //Unlike most instancs of Think(), this action must be taken at the reflexspeed the Being originally rolled at the start of the round
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i].ability.abilityType == AbilityType.PublicNormal)
                {
                    tempList[i].reflex = LIST1[currentTurn].reflexSpeed;
                }
            }
            ProposeActions(tempList);
        }
    }

    public void ProposeActions(List<Thought> thoughts)
    {
        LIST2.AddRange(thoughts);
        SortList2();
    }

    private void SortList1()
    {
        List<BeingToken> sortedList = LIST1.OrderByDescending(o => o.reflexSpeed).ToList();
        LIST1 = sortedList;
    }

    private void SortList2()
    {
        //Should put instant actions on top etc, so Order by.andThen.etc
        List<Thought> SortedList = LIST2.OrderByDescending(o => o.reflex).ToList();
        LIST2 = SortedList;
    }

    private void CommitToAction()
    {
        for (int i = LIST2.Count - 1; i > -1; i--)
        {
            if (LIST2[i].ability.GetParentBeing().isCommittedToAction)
            {
                LIST2.RemoveAt(i);
            }
        }

        if (LIST2.Count > 0)
        {
            //Take action

            LIST2[0].ability.GetParentBeing().isCommittedToAction = true;//this should only happen when it's a PUBLICNORMAL abilty
            currentThought = LIST2[0];
            visualManager.VisualiseThought(LIST2);
            //Move thought from list 2 to become an action in list 3: 'X dashes forwards'
            Action a = LIST2[0].ability.GetParentBeing().Act(LIST2[0]);
            LIST3.Add(a);
            //find action 'a' that were currently moving from Thought to Action in LIST3 - it might not be in element 0
            currentAction = LIST3.Find(action => action == a); 
            visualManager.VisualiseAction(a);
            SortList3();
            LIST2.RemoveAt(0);
            //Get reactions from capable Beings
            List<Thought> tempList = new List<Thought>();
            for (int i = 0; i < LIST1.Count; i++)
            {
                if (LIST1[i].being.isCommittedToAction == false) //May change this later, either put it in the Being or think about situations where they can't return a PublicAction but can a mental or instant action
                {
                    //Debug.Log(LIST1[i].being.beingName + " is thinking about a response");
                    tempList.AddRange(LIST1[i].being.React(a));
                }
            }
            //Remove reactions that are slower than the original actions (currentAction)'s reflex
            for (int i = tempList.Count - 1; i > -1; i--)
            {
                if (tempList[i].reflex < currentThought.reflex)
                {
                    //Debug.Log(tempList[i].ability.GetParentBeing().beingName + " " + tempList[i].ability.abilityName +" " + tempList[i].reflex + " is less than " + currentAction.reflex);
                    tempList.RemoveAt(i);
                }
            }
            LIST2.AddRange(tempList);
            SortList2();
            //perhaps remove all commited Beungs from LIST2 at this point

        }
        else
        {
            step = 2;
        }

    }

    private void SortList3()
    {

        List<Action> SortedList = LIST3.OrderByDescending(o => o.toHit).ToList();
        LIST3 = SortedList;
    }

    private void TakeAction()
    {
        if (LIST3.Count != 0)
        {
            if (LIST3[0].ability.CanThisBeUsed(this))
            {
                currentAction = LIST3[0];
                //calculate tohit values
                //TODO
                //get compare to get attackers to hit advantage
                attackersFavour = chassisTable.CompareToHits(currentAction);
                //Compare Chassis rules to see if a special effect happens like a beam battle
                visualManager.VisualiseResolution();
                //chassisTable.CheckChassisTable();
                //Debug.Log(LIST3[0].ability.GetParentBeing().beingName + " uses " + LIST3[0].ability.abilityName);
                //LIST3[0].ability.Use();
            }
            LIST3.RemoveAt(0);
        }
        else
        {
            Debug.Log("List 3 empty --> step 3");
            step = 3;
        }
    }

    private Action CalculateToHit(Action a)
    {
        if (a == null)
        {
            Debug.Log("Error: CalculateToHit passed null instead of Action");
        }
        else
        {
            if (a.actionType == ThoughtType.Normal)
            {

            }
            if (a.actionType == ThoughtType.Reaction)
            {

            }
        }

        return a;
    }

    private void NextTurn()
    {
        Debug.Log("next turn");
        //Everyone becomes un-commited, I'm doing this here to avoid making an end of turn behaviour that does it, behaviours are diagetic, rules are not
        for (int i = 0; i < LIST1.Count; i++)
        {
            LIST1[i].being.isCommittedToAction = false;
        }

        if (currentTurn < LIST1.Count -1)
        {
            Debug.Log("current turn is " + currentTurn + " out of " + LIST1.Count);
            currentTurn++;
            step = 0;
        }
        else
        {
            Debug.Log("New round");
            step = 4;
        }
        
    }

    private void NewRound()
    {
        Debug.Log("END OF ROUND");
    }
}
