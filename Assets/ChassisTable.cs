using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;


[CreateAssetMenu]
public class ChassisTable : ScriptableObject
{

    public ActionManager actionManager;
    public TextAsset chassisRulesCSV;
    List<ChassisRule> chassisRules = new List<ChassisRule>();

    public float attackersfavour = 0;

    float criticalFail = -100;
    float fail = -75;
    float lowerEqual = -25;
    float higherEqual = 25;
    float success = 75;
    float criticalSuccess = 100;

    public void LoadChassisRulesCSV()
    {
        chassisRules = LoadChassisRules(chassisRulesCSV);
        chassisRules = SortListBySalience(chassisRules);
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
            }
        }
        
        else
        {
            Debug.Log("Error: txt asset is null");
        }

        Debug.Log(chassisRules.Count + " Chassis rules loaded");
        return chassisRules;
      
    }

    private ChassisCondition CreateChassisCondition(string[] name)
    {
        if (name != null)
        {

            if (name[0] == "AttackersFavour")
            {
                if (name[1] != null)
                {
                    int param;

                    if (int.TryParse(name[1], out param))
                    {
                        if (actionManager != null)
                        {
                            return new AttackersFavour_ChassisCondition(actionManager, param);
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
                    Debug.Log("Error: No int given for AttackersFavour ChassisCondition creation");
                    return null;
                }
            }


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


            if (name[0] == "TargetCount")
            {
                if (name[1] != null)
                {
                    int param;

                    if (int.TryParse(name[1], out param))
                    {
                        if (actionManager != null)
                        {
                            return new TargetCount_ChassisCondition(actionManager, param);
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
                    Debug.Log("Error: No int given for TargetCount ChassisCondition creation");
                    return null;
                }
            }

            if (name[0] == "ProvokerCount")
            {
                if (name[1] != null)
                {
                    int param;

                    if (int.TryParse(name[1], out param))
                    {
                        if (actionManager != null)
                        {
                            return new ProvokerCount_ChassisCondition(actionManager, param);
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
                    Debug.Log("Error: No int given for ProvokerCount ChassisCondition creation");
                    return null;
                }
            }


            if (name[0] == "ProvokerIsSoleTarget")
            {
                if (name.Length == 1)
                {   
                   if (actionManager != null)
                   {
                      return new ProvokerIsSoleTarget_ChassisCondition(actionManager);
                   }
                   Debug.Log("Error: ChassisTable has no reference to action manager");
                   return null;   
                }
                else
                {
                Debug.Log("Error: parameter given for ProvokerIsTarget when none is required");
                return null;
                }
            }


            if (name[0] == "ActionType")
            {
                if (name[1] != null)
                {
                    if (name[1] == "Normal")
                    {
                        if (actionManager != null)
                        {
                            return new ActionType_ChassisCondition(actionManager, ThoughtType.Normal );
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Reaction")
                    {
                        if (actionManager != null)
                        {
                            return new ActionType_ChassisCondition(actionManager, ThoughtType.Reaction);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    
                    {
                        Debug.Log("Error: Could not parse param - should be a ThoughtType eg Normal or Reaction etc ");
                        return null;
                    }
                }
                else
                {
                    Debug.Log("Error: No actionType given for ActionType ChassisCondition creation");
                    return null;
                }
            }


            if (name[0] == "ActorChassis")
            {
                if (name[1] != null)
                {
                    if (name[1] == "Melee")
                    {
                        if (actionManager != null)
                        {
                            return new ActorChassis_ChassisCondition(actionManager, AbilityChassis.Melee);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Block")
                    {
                        if (actionManager != null)
                        {
                            return new ActorChassis_ChassisCondition(actionManager, AbilityChassis.Block);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Dodge")
                    {
                        if (actionManager != null)
                        {
                            return new ActorChassis_ChassisCondition(actionManager, AbilityChassis.Dodge);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Beam")
                    {
                        if (actionManager != null)
                        {
                            return new ActorChassis_ChassisCondition(actionManager, AbilityChassis.Beam);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Ball")
                    {
                        if (actionManager != null)
                        {
                            return new ActorChassis_ChassisCondition(actionManager, AbilityChassis.Ball);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "BigBall")
                    {
                        if (actionManager != null)
                        {
                            return new ActorChassis_ChassisCondition(actionManager, AbilityChassis.BigBall);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Blast")
                    {
                        if (actionManager != null)
                        {
                            return new ActorChassis_ChassisCondition(actionManager, AbilityChassis.Blast);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Wave")
                    {
                        if (actionManager != null)
                        {
                            return new ActorChassis_ChassisCondition(actionManager, AbilityChassis.Wave);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }

                    {
                        Debug.Log("Error: Could not parse string in actor chassis should be an ability chassis type eg Beam or Block etc string given: " + name[1]);
                        return null;
                    }
                }
                else
                {
                    Debug.Log("Error: No chassis type given for Actor Chassis ChassisCondition creation");
                    return null;
                }
            }


            if (name[0] == "ProvokerChassis")
            {
                if (name[1] != null)
                {
                    if (name[1] == "Melee")
                    {
                        if (actionManager != null)
                        {
                            return new ProvokerChassis_ChassisCondition(actionManager, AbilityChassis.Melee);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Block")
                    {
                        if (actionManager != null)
                        {
                            return new ProvokerChassis_ChassisCondition(actionManager, AbilityChassis.Block);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Dodge")
                    {
                        if (actionManager != null)
                        {
                            return new ProvokerChassis_ChassisCondition(actionManager, AbilityChassis.Dodge);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Beam")
                    {
                        if (actionManager != null)
                        {
                            return new ProvokerChassis_ChassisCondition(actionManager, AbilityChassis.Beam);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Ball")
                    {
                        if (actionManager != null)
                        {
                            return new ProvokerChassis_ChassisCondition(actionManager, AbilityChassis.Ball);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "BigBall")
                    {
                        if (actionManager != null)
                        {
                            return new ProvokerChassis_ChassisCondition(actionManager, AbilityChassis.BigBall);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Blast")
                    {
                        if (actionManager != null)
                        {
                            return new ProvokerChassis_ChassisCondition(actionManager, AbilityChassis.Blast);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }
                    if (name[1] == "Wave")
                    {
                        if (actionManager != null)
                        {
                            return new ProvokerChassis_ChassisCondition(actionManager, AbilityChassis.Wave);
                        }
                        Debug.Log("Error: ChassisTable has no reference to action manager");
                        return null;
                    }

                    {
                        Debug.Log("Error: Could not parse string in provoker chassis should be an ability chassis type eg Beam or Block etc string given: " + name[1]);
                        return null;
                    }
                }
                else
                {
                    Debug.Log("Error: No chassis type given for Provoker Chassis ChassisCondition creation");
                    return null;
                }
            }

        }

        Debug.Log("Error: Cannot create ChassisCondition - name is null");
        return null;
    }

    public int CompareToHits(Action action)
    {
       //Debug.Log("comparing to hits");
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
        if (actionManager.currentAction != null)
        {
            float a = CompareToHits(actionManager.currentAction);
           // Debug.Log(actionManager.currentAction.actors[0].beingName + "'s actonFacour is " + a);
            actionManager.attackersFavour = a;
        }
        else
        {
            Debug.Log("Error: actionManager.currentAction = null");
        }


        if (chassisRules != null)
        {
            for (int i = 0; i < chassisRules.Count; i++)
            {

                if (chassisRules[i].CanThisBeUsed())
                {
                    string s = chassisRules[i].visual;

                    if (actionManager.currentAction.actionType == ThoughtType.Reaction)
                    {
                        s = InsertNames(s, actionManager.currentAction.actors[0].beingName,actionManager.currentAction.targets[0].beingName, actionManager.currentAction.provoker.actors[0].beingName);
                        Debug.Log(s);
                    }
                    else
                    {
                        s = InsertNames(s, actionManager.currentAction.actors[0].beingName, actionManager.currentAction.targets[0].beingName, "no provoker");
                        Debug.Log(s);
                    }
                    //Only one visual fires with this method, the most specific case
                    return;
                } 
            }
        }
    }

    private List<ChassisRule> SortListBySalience(List<ChassisRule> list)
    {
        List<ChassisRule> SortedList = list.OrderByDescending(o => o.conditions.Count).ToList();
        return SortedList;
    }

    private string InsertNames(string s, string attacker, string target, string provoker)
    {
        s = s.Replace("ACTOR", attacker);
        s = s.Replace("TARGET", target);
        s = s.Replace("PROVOKER", provoker);

        return s;
    }







    //this is now obsolete
    /*
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


    */





}
