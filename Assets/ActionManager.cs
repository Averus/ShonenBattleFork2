using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum CombatState
{
    Activate = 100,
    AbilitySpeedCalc = 200,
    ToHitCalc = 300,
    Hit = 400,
    Miss = 500,
    LateEffects = 600,
}

public class ActionManager : MonoBehaviour {

    public VisualManager visualManager;
    public ChassisTable chassisTable;
    public RuleFunctions ruleFunctions;
    public Debug_Visualiser debug_Visualiser; //This is a temporary script that handles displaying the 'visuals' from the visualManager while they're still text strings
    public bool turnNotFinishedYet = false;

    public Canvas canvas;
    public GameObject selectAbilitiesMenuPrefab;//used when player must select abilities to use
    public GameObject button; //used when player must select from some options

    public List<BeingToken> LIST1 = new List<BeingToken>(); //A list of beings acting in the location
    public List<Thought> LIST2 = new List<Thought>();//A list of actions declared by said beings ready to be ordered and added to...
    public List<Action> LIST3 = new List<Action>(); //The main stack of public actions, ready to be resolved one by one

    public int currentTurn = 0;
    public int step = 0;
    public bool pause = false;
    public CombatState combatState = 0;
    public List<EffectToken> effectsInPlay = new List<EffectToken>();

    public Thought currentThought = null; //The current Thought that is being evaluated
    public Action currentAction = null; //The current action being evaluated
    public float attackersFavour = 0; //Actors speed value

    //public Thought playerSelectedAbility = null; //The ability chosen by the player
    public List<Thought> playerSelectedAbilities = null; //a package of abilities selected by the player on the current turn

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

        if (Input.GetKeyDown(KeyCode.Space) && pause == false)
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
               Step1(); //fires the turn of the current Being, asking them to Think() and filling LIST2
                pause = true;
            }




        }

        if (Input.GetKeyUp(KeyCode.Space) && pause == true)
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
        //turnNotFinishedYet = true; //not sure what this was for.
        if (LIST1.Count > 0)
        {


            //IF BEING IS PLAYER CONTROLLED
            if (LIST1[currentTurn].being.playerControlled == true)
            {
                Debug.Log("IS PLAYER CONTROLED");

                //create a 'Select Abilities' menu for the player to use
                GameObject scrollView = GameObject.Instantiate(selectAbilitiesMenuPrefab, canvas.transform);
                //initialize the psa - set ThoughtType as normal as this will not be a reaction (it's on the beings combat turn)
                //Unlike in other cases, any publicnormal abilities here must be taken at the reflexspeed the Being originally rolled at the start of the round
                scrollView.GetComponent<PlayerSelectAbilities>().Initialize(this, LIST1[currentTurn].being, ThoughtType.Normal, LIST1[currentTurn].reflexSpeed);
                
                //from here the menu object will handle collecting abilities and targets etc, then will fire the next step in ActionManager itself.
            }

            //IF BEING IS NOT PLAYER CONTROLLED
            if (LIST1[currentTurn].being.playerControlled == false)
            {
                //ask Being to Think of some abilities to use
                List<Thought> tempList = LIST1[currentTurn].being.Think();

                if (tempList != null)
                {
                    if (tempList.Count > 0)
                    {
                        //Unlike most instances of Think(), this action must be taken at the reflexspeed the Being originally rolled at the start of the round
                        for (int i = 0; i < tempList.Count; i++)
                        {
                            if (tempList[i].ability.abilityType == AbilityType.PublicNormal)
                            {
                                tempList[i].reflex = LIST1[currentTurn].reflexSpeed;
                            }
                        }
                        //ProposeActions(tempList);
                    }
                }
                ProposeActions(tempList);
            }


            
        }
    }

    public void ProposeActions(List<Thought> thoughts)
    {
        if (thoughts != null)
        {
            LIST2.AddRange(thoughts);
            SortList2();
        }
        
        step = 1;
    }

    
    /*
    public void CheckPlayerSelectedAbility() //This is called from the ability selection popup (the script on the button sets the playerSelectedAbility to whatever ability)
    {
        if (playerSelectedAbility == null)
        {
            Debug.Log(LIST1[currentTurn].being.beingName + " chose to do nothing");
            step = 1;
        }
        if (playerSelectedAbility != null)
        {
            if (playerSelectedAbility.targets == null)
            {
                DisplayPossibleTargets();
            }
        }
    }

    private void DisplayPossibleTargets()
    {
        if (playerSelectedAbility == null)
        {
            Debug.Log("Error: Cannot select targets as there is no playerSelectedAbility");
            step = 1;
            return;
        }

        GameObject scrollview = GameObject.Instantiate(scrollViewPrefab, canvas.transform);
        Transform content = scrollview.transform.GetChild(0).GetChild(0);

        if (playerSelectedAbility.targets.Count < playerSelectedAbility.ability.numberOfTargets)
        {

            for (int i = 0; i < playerSelectedAbility.ability.validTargets.Count; i++)
            {
                //put a button in the list
                GameObject b = GameObject.Instantiate(button, content);
                //space each button out by height
                b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y - (25 * i), b.transform.position.z);
                //give the button a reference to a valid target
                b.GetComponent<ButtonBehaviours>().SetAsTargetButton(playerSelectedAbility.ability.validTargets[i]);
                //Set the buttons onClick behaviour to fire its own ButtonCLicked method from its own ButtonBehaviours script (because each button is initialized differently)
                //This onClick behaviour does not show in the inspector for some reason, but it does work
                b.GetComponent<Button>().onClick.AddListener(b.GetComponent<ButtonBehaviours>().ButtonClicked);
            }
 
        }
        else
        {
            step = 1;
        }


    }

    */

    private void SortList1()
    {
        //Debug.Log("sortin list 1");
        List<BeingToken> sortedList = LIST1.OrderByDescending(o => o.reflexSpeed).ToList();
        LIST1 = sortedList;
    }

    private void SortList2()
    {
        //Should put instant actions on top etc, so Order by.andThen.etc
        List<Thought> SortedList = LIST2.OrderBy(o => o.ability.abilityType).ThenByDescending(o => o.reflex).ToList();
        LIST2 = SortedList;
    }

    private void ActivateEffects(Action action)
    {
        for (int i = 0; i < action.ability.effects.Count; i++)
        {
            if (action.ability.effects[i].UsedInState == combatState)
            {
                EffectToken effectToken = new EffectToken(action.ability.effects[i], action.targets);
                
                effectsInPlay.Add(effectToken);
            }
        }
    }

    private void SortEffectsInPlay()
    {
        //sorts by resolution order so ResolveEffectsInPlay can simply fire along the line
    }

    private void ResolveEffectsInPlay()
    {
        for (int i = 0; i < effectsInPlay.Count; i++)
        {
            if (effectsInPlay[i].effect.UsedInState == combatState)
            {
                for (int ii = 0; ii < effectsInPlay[i].targets.Count; ii++)
                {
                    Debug.Log("Doing effect " + effectsInPlay[i].effect.effectName + " from " + effectsInPlay[i].effect.parentAbility + " in " + effectsInPlay[i].effect.parentBeing.beingName);
                    effectsInPlay[i].effect.Use(effectsInPlay[i].targets[ii]);
                    effectsInPlay[i].effect.persistsForTurns -= 1;


                }
            }
        }
        //false because it's not yet the end of the round
        RemoveSpentEffects(false);
    }

    public void UseCurrentActionActorEffects(CombatState combatState)
    {
        Debug.Log("USECURRENTACTIONACTOREFFECTS: " + combatState.ToString());
        //We begin calculating the abilities speed
        this.combatState = combatState;
        //Place effects that fire in the current state into the EffectsInPlay list
        ActivateEffects(currentAction);
        //Sort the list by resolution order
        SortEffectsInPlay(); //TODO
                             //Resolve the list
        ResolveEffectsInPlay();


    }

    public void UseCurrentActionProvokerEffects(CombatState combatState)
    {
        //We begin calculating the abilities speed
        this.combatState = combatState;
        //Place effects that fire in the current state into the EffectsInPlay list
        if (currentAction.provoker != null)
        {
            ActivateEffects(currentAction.provoker);
        }
        else
        {
            Debug.Log("Error: UseCurrentAbilityProvokerEffects was called but currentActon.provoker is null");
        }
        //Sort the list by resolution order
        SortEffectsInPlay(); //TODO
                             //Resolve the list
        ResolveEffectsInPlay();
    }

    public void UseCurrentActionActorAndProvokerEffects(CombatState combatState)
    {
        //We begin calculating the abilities speed
        this.combatState = combatState;
        //Place effects that fire in the current state into the EffectsInPlay list
        ActivateEffects(currentAction);
        //Also for the provoker
        if (currentAction.provoker != null)
        {
            ActivateEffects(currentAction.provoker);
        }
        else
        {
            Debug.Log("Error: UseCurrentAbilityProvokerEffects was called but currentActon.provoker is null");
        }
        //Sort the list by resolution order
        SortEffectsInPlay(); //TODO
                             //Resolve the list
        ResolveEffectsInPlay();
    }

    public void UseCurrentActionActorAndTargetEffects(CombatState combatState)
    {
        //We begin calculating the abilities speed
        this.combatState = combatState;
        //Place effects that fire in the current state into the EffectsInPlay list
        ActivateEffects(currentAction.provoker);
        //Sort the list by resolution order
        SortEffectsInPlay(); //TODO
                             //Resolve the list
        ResolveEffectsInPlay();
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
            currentThought = LIST2[0];
            //If the current action is a PublicNormal, set that being as Committed to action - whether it's their turn or not (in the case of a reaction)
            if (currentThought.ability.abilityType == AbilityType.PublicNormal)
            {
                LIST2[0].ability.GetParentBeing().isCommittedToAction = true;
            }
            //Move thought from list 2 to become an action in list 3: 'X dashes forwards'
            Action a = LIST2[0].ability.GetParentBeing().Act(LIST2[0]);
            LIST3.Add(a);
            //find action 'a' that were currently moving from Thought to Action in LIST3 - it might not be in element 0
            currentAction = LIST3.Find(action => action == a);
            //Play the 'activate' visual of the ability
            debug_Visualiser.Debug_DisplayNewVisuals(visualManager.Visualise(a));
            SortList3();
            LIST2.RemoveAt(0);

            //look for and resolve effects that fire on Activate in current ability - for example an MP cost for using this ability
            UseCurrentActionActorEffects(CombatState.Activate);

            //look for and resolve effects that fire on AbilitySpeedCalc in current ability
            UseCurrentActionActorEffects(CombatState.AbilitySpeedCalc);

            //Get reactions from capable Beings
            List<Thought> tempList = new List<Thought>();
            for (int i = 0; i < LIST1.Count; i++)
            {
                if (LIST1[i].being.isCommittedToAction == false) //May change this later, either put it in the Being or think about situations where they can't return a PublicAction but can a mental or instant action
                {
                    //Debug.Log(LIST1[i].being.beingName + " is thinking about a response");
                    List<Thought> safteyList = LIST1[i].being.React(a);
                    if (safteyList != null)
                    {
                        tempList.AddRange(safteyList);
                    }
                }
            }
            //Remove reactions that are slower than the original actions (currentAction)'s action speed (or tohit whatever I'm calling it)
            for (int i = tempList.Count - 1; i > -1; i--)
            {
                if (tempList[i].reflex < currentAction.toHit)
                {
                    //Debug.Log(tempList[i].ability.GetParentBeing().beingName + " " + tempList[i].ability.abilityName +" " + tempList[i].reflex + " is less than " + currentAction.toHit);
                    tempList.RemoveAt(i);
                }
            }
            LIST2.AddRange(tempList);
            SortList2();
            //perhaps remove all commited Beings from LIST2 at this point

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

                if (IsFirstLIST3ProvokerStillActive())
                {
                    
                    //compare tohits to get attackers to hit advantage
                    attackersFavour = chassisTable.CompareToHits(currentAction);

                    debug_Visualiser.Debug_DisplayNewVisuals(visualManager.VisualiseResolution());
                    //Compare Chassis rules to see if a special effect happens like a beam battle
                    chassisTable.CheckChassisTable();

                    //Functions get called from the chassisTable like 'UseCurrentActionActorEffects' etc which fire the 'on hit' effects like damage etc

                    //we ask each Being to check if it's state has changed and do anything about it that it wants to eg dying if HP is now 0
                    for (int i = 0; i < LIST1.Count; i++)
                    {
                        LIST1[i].being.resolutionFunction.Use();
                    }

                    combatState = CombatState.LateEffects;
                    //Fire the late effects which should be the last items in the effectsInPlay list (?) eg knockback working - things that happen at the end of a turn
                    ResolveEffectsInPlay();
                }
            }
            LIST3.RemoveAt(0);
        }
        else
        {
            Debug.Log("List 3 empty --> step 3");
            step = 3;
        }
    }

    private bool IsFirstLIST3ProvokerStillActive()
    {
        if (LIST3[0].actionType == ThoughtType.Reaction)
        {
            if (LIST3.Contains(LIST3[0].provoker))
            {
                //Debug.Log("Action: " + LIST3[0].provoker.actors[0].beingName + LIST3[0].provoker.ability.abilityName + LIST3[0].provoker.targets[0].beingName + " still exists");
                return true;                
            }
            else
            {
                //Debug.Log("Action: " + LIST3[0].provoker.actors[0].beingName + LIST3[0].provoker.ability.abilityName + LIST3[0].provoker.targets[0].beingName + " no longer exists");
                return false;
            }
        }
        return true;
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

    public float GetPowerLevelComparison(Being origin, Being comparator)
    {
        return origin.GetResourceValue("POWERLEVEL", 1) / comparator.GetResourceValue("POWERLEVEL", 1);
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

    private void RemoveSpentEffects(bool endOfRound)
    {
        if (!endOfRound)
        {
            for (int i = effectsInPlay.Count - 1; i > -1; i--)
            {
                //checks the effects persists for TURNS value
                if (effectsInPlay[i].effect.persistsForTurns <= 0)
                {
                    effectsInPlay.RemoveAt(i);
                }
            }

        }

        if (endOfRound)
        {
            for (int i = effectsInPlay.Count - 1; i > -1; i--)
            {
                //checks the effects persists for ROUNDS value
                if (effectsInPlay[i].effect.persistsForRounds <= 0)
                {
                    effectsInPlay.RemoveAt(i);
                }
            }
        }

    }

    private void UnCommitBeings()
    {
        //What about beings in a beam battle etc? They should stay committed.
        for (int i = 0; i < LIST1.Count; i++)
        {
            LIST1[i].being.isCommittedToAction = false;
        }
    }

    private void NewRound()
    {
        for (int i = 0; i < effectsInPlay.Count; i++)
        {
            effectsInPlay[i].effect.persistsForRounds--;
        }
        RemoveSpentEffects(true);
        UnCommitBeings();
        currentTurn = 0;
        step = 0;
    }
}
