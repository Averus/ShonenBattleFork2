using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu]
public class ChassisTable : ScriptableObject
{

    public ActionManager actionManager;
    public TextAsset chassisRulesCSV;
    List<ChassisRule> chassisRules = new List<ChassisRule>();

    float criticalFail = -100;
    float fail = -75;
    float lowerEqual = -25;
    float higherEqual = 25;
    float success = 75;
    float criticalSuccess = 100;

    public void LoadChassisRulesCSV()
    {
        chassisRules = LoadChassisRules(chassisRulesCSV);
    }

    private List<ChassisRule> LoadChassisRules( TextAsset txt)
    {

        List<ChassisRule> chassisRules = new List<ChassisRule>();

        if (txt != null)
        {
            Debug.Log("Loading chassis rules from csv");
            StreamReader reader = new StreamReader(new MemoryStream(txt.bytes));

            while(reader.Peek() != -1)
            {
                ChassisRule rule = new ChassisRule();
                string s = reader.ReadLine(); //one entire rule
                string[] arr = s.Split(','); //split into conditions, visual, effects etc
                for (int i = 0; i < arr.Length; i++)
                {

                    if (arr[i].StartsWith("\""))
                    {
                        rule.visual = arr[i];
                    }
                    else
                    {
                        string[] ob = arr[i].Split();
                        ChassisCondition cc = CreateChassisCondition(ob);

                        if (cc != null)
                        {
                            rule.conditions.Add(cc);
                        }
                    }
                }
                chassisRules.Add(rule);
                Debug.Log("ChassisRule length " + chassisRules.Count);
            }
        }
        
        else
        {
            Debug.Log("Error: txt asset is null");
        }

        return chassisRules;
      
    }

    private ChassisCondition CreateChassisCondition(string[] name)
    {
        if (name != null)
        {
            if (name[0] == "ActorCount")
            {
                if (name[1] != null)
                {
                    int param;

                    if (int.TryParse(name[1], out param))
                    {
                        if (actionManager != null)
                        {
                            return new ActorCount_ChassisCondition(actionManager, param);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    else
                    {
                        Debug.Log("Error: Could not parse param - should be an int");
                        return null;
                    }
                }
                else
                {
                    Debug.Log("Error: No int given for ActorCount ChassisCondition creation");
                    return null;
                }
            }

            Debug.Log("Error: ChassisCondition creation name not recognised");
            return null;
        }

        Debug.Log("Error: Cannot create ChassisCondition - name is null");
        return null;
    }





    public float attackersfavour = 0; //for debug to see in inspector

    private int CompareToHits(Action action)
    {
       // Debug.Log("comparing to hits");
        //REACTIONS - CONTESTED ABILITIES - EXCHANGES WITH BOTH HALVES
        if (action.actionType == ThoughtType.Reaction)
        {
            //ONE ATTACKER
            if (action.actors.Count == 1)
            {
                //ONE DEFENDER/TARGET
                if (action.targets.Count == 1)
                {
                    //ONE PROVOKING ATTACKER
                    if (action.provoker.actors.Count == 1)
                    {

                            float attackersFavour = ((action.provoker.toHit / action.toHit) * 100) - 100;

                            if (attackersFavour <= criticalFail)
                            {
                                return 1;
                            }

                            if (attackersFavour > criticalFail && attackersFavour <= fail)
                            {
                                return 2;
                            }

                            if (attackersFavour > fail && attackersFavour <= success)
                            {
                                return 3;
                            }

                            if (attackersFavour > success && attackersFavour <= criticalSuccess)
                            {
                                return 4;
                            }

                            if (attackersFavour > criticalSuccess)
                            {
                                return 5;
                            }
                    }
                }
            }
        }

        //NORMAL ATTACKS WITH NO CONTESTED ABILITY
        if (action.actionType == ThoughtType.Normal)
        {
            //ONE ATTACKER
            if (action.actors.Count == 1)
            {
                //ONE DEFENDER/TARGET
                if (action.targets.Count == 1)
                {
 
                            float attackersFavour = ((1 / action.toHit) * 100) - 100;

                            if (attackersFavour <= criticalFail)
                            {
                                return 1;
                            }

                            if (attackersFavour > criticalFail && attackersFavour <= fail)
                            {
                                return 2;
                            }

                            if (attackersFavour > fail && attackersFavour <= success)
                            {
                                return 3;
                            }

                            if (attackersFavour > success && attackersFavour <= criticalSuccess)
                            {
                                return 4;
                            }

                            if (attackersFavour > criticalSuccess)
                            {
                                return 5;
                            }  
                }
            }
        }

        return 0;
    }


    public void CheckChassisTable()
    {
        if (chassisRules != null)
        {
            for (int i = 0; i < chassisRules.Count; i++)
            {
                if (chassisRules[i].CanThisBeUsed())
                {
                Debug.Log(chassisRules[i].visual);
                }
                
            }
        }
    }

    //this is now obsolete
    public void Resolve(Action action)
    {
        //Debug.Log("resolving");

        float attackersFavour = CompareToHits(action);
        attackersfavour = attackersFavour;

        //REACTIONS - CONTESTED ABILITIES - EXCHANGES WITH BOTH HALVES
        if (action.actionType == ThoughtType.Reaction)
        {
            //ONE ATTACKER
            if (action.actors.Count == 1)
            {
                //ONE DEFENDER/TARGET
                if (action.targets.Count == 1)
                {
                    //ONE PROVOKING ATTACKER
                    if (action.provoker.actors.Count ==1)
                    {
                        //IF TARGET IS THE SAME AS THE PROVOKING BEING
                        if (action.targets[0] == action.provoker.actors[0])
                        {
                            //MELEE ATTACK
                            if (action.ability.abilityChassis == AbilityChassis.Melee)
                            {
                                //MELEE DEFENDER
                                if (action.provoker.ability.abilityChassis == AbilityChassis.Melee)
                                {

                                    if (attackersFavour == 1)
                                    {
                                        string s = action.provoker.actors[0].beingName + " hits only " + action.actors[0].beingName + "'s afterimage before they are hit square in the chest";
                                        Debug.Log(s);
                                    }                                    
                                    if (attackersFavour == 2)
                                    {
                                        string s = action.provoker.actors[0].beingName + "'s punch goes wide and " + action.actors[0].beingName + " lands a hit square in their stomach";
                                        Debug.Log(s);
                                    }
                                    if (attackersFavour == 3)
                                    {
                                        string s = action.provoker.actors[0].beingName + " and " + action.actors[0].beingName + " simultaniously slam fists into eachothers faces";
                                        Debug.Log(s);
                                    }
                                    if (attackersFavour == 4)
                                    {
                                        string s =  action.actors[0].beingName + "'s punch goes wide and " + action.provoker.actors[0].beingName + " lands a hit square in their stomach";
                                        Debug.Log(s);
                                    }
                                    if (attackersFavour == 5)
                                    {
                                        string s = action.actors[0].beingName + " hits only " + action.provoker.actors[0].beingName + "'s afterimage before they are hit square in the chest";
                                        Debug.Log(s);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        //NORMAL UNCONTESTED ATTACKS/ ABILITIES
        if (action.actionType == ThoughtType.Normal)
        {
            //ONE ATTACKER
            if (action.actors.Count == 1)
            {

                //ONE DEFENDER/TARGET
                if (action.targets.Count == 1)
                {

                    //MELEE ATTACK

                    if (action.ability.abilityChassis == AbilityChassis.Melee)
                          {
                                    if (attackersFavour == 1)
                                    {
                                        string s = action.actors[0].beingName + " punches " + action.targets[0].beingName + " square in the face";
                                        Debug.Log(s);
                                    }
                                    if (attackersFavour == 2)
                                    {
                                          string s = action.actors[0].beingName + " punches " + action.targets[0].beingName + " square in the face";
                                          Debug.Log(s);
                                    }
                                    if (attackersFavour == 3)
                                    {
                                           string s = action.actors[0].beingName + " punches " + action.targets[0].beingName + " square in the face";
                                           Debug.Log(s);
                                    }
                                    if (attackersFavour == 4)
                                    {
                                        string s = action.actors[0].beingName + " punches " + action.targets[0].beingName + " square in the face";
                                        Debug.Log(s);
                                    }
                                    if (attackersFavour == 5)
                                    {
                                        string s = action.actors[0].beingName + " blindsides " + action.targets[0].beingName + " with a punch before they can even react";
                                        Debug.Log(s);
                                    }
                             }
                            
          
                }
            }

        }
    }









    private bool CurrentActionIsType(int thoughtType)
    {
        if (actionManager.currentAction != null)
        {
            if (actionManager.currentAction.actionType == (ThoughtType)thoughtType)
            {
                return true;
            }
        }
        return false;
    }

    private bool CurrentActionActorsCount(int number)
    {
        if (actionManager.currentAction != null)
        {
            if (actionManager.currentAction.actors.Count == number)
            {
                return true;
            }
        }
        return false;
    }

    private bool CurrentActionTargetsCount(int number)
    {
        if (actionManager.currentAction != null)
        {
            if (actionManager.currentAction.targets.Count == number)
            {
                return true;
            }
        }
        return false;
    }

    private bool ProvokingActorsCount(int number)
    {
        if (actionManager.currentAction != null)
        {
            if (actionManager.currentAction.provoker != null)
            {
                if (actionManager.currentAction.provoker.actors.Count == number)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CurrentActionTargetIsSoleProkover(int number)
    {
        if (actionManager.currentAction != null)
        {
            if (actionManager.currentAction.provoker != null)
            {
                if (actionManager.currentAction.targets[0] == actionManager.currentAction.provoker.actors[0])
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool AttackersFavourRatingIs(int number)
    {
        //if (actionManager.attackersFavourRating == number)
      //  {
       //     return true;
      //  }
        return false;
    }

    private bool CurrentActionAttackerChassisIs(int chassisType)
    {
        if (actionManager.currentAction.ability.abilityChassis == (AbilityChassis)chassisType)
        {
            return true;
        }
        return false;
    }

    private bool CurrentActionProvokerChassisIs(int chassisType)
    {
        if (actionManager.currentAction.provoker.ability.abilityChassis == (AbilityChassis)chassisType)
        {
            return true;
        }
        return false;
    }

}
