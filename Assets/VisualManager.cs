using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class VisualManager : ScriptableObject {

    public ActionManager actionManager;
    //visuals for starting an action i.e 'jumping forwards'
    public TextAsset defaultChassisVisualsCSV;
    public TextAsset resolutionVisualsCSV;
    List<ChassisRule> defaultChassisVisuals = new List<ChassisRule>();
    List<ChassisRule> resolutionVisuals = new List<ChassisRule>();

    public void LoadDefaultChassisVisualsCSV()
    {
        defaultChassisVisuals = LoadDefaultChassisVisuals(defaultChassisVisualsCSV);
        defaultChassisVisuals = SortListBySalience(defaultChassisVisuals);
    }

    private List<ChassisRule> LoadDefaultChassisVisuals(TextAsset txt)
    {

        List<ChassisRule> chassisRules = new List<ChassisRule>();

        if (txt != null)
        {
            Debug.Log("Loading default chassis visuals from csv");
            StreamReader reader = new StreamReader(new MemoryStream(txt.bytes));

            while (reader.Peek() != -1)
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
            Debug.Log(chassisRules.Count + " default chassis visuals loaded");
        }

        else
        {
            Debug.Log("Error: txt asset is null");
        }

        return chassisRules;

    }

    public void LoadResolutionVisualsCSV()
    {
        resolutionVisuals = LoadResolutionVisuals(resolutionVisualsCSV);
        resolutionVisuals = SortListBySalience(resolutionVisuals);
    }

    private List<ChassisRule> LoadResolutionVisuals(TextAsset txt)
    {

        List<ChassisRule> chassisRules = new List<ChassisRule>();

        if (txt != null)
        {
            Debug.Log("Loading resolution visuals from csv");
            StreamReader reader = new StreamReader(new MemoryStream(txt.bytes));

            while (reader.Peek() != -1)
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
            Debug.Log(chassisRules.Count + " resolution visuals loaded");
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
                            return new ActionType_ChassisCondition(actionManager, ThoughtType.Normal);
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

    private void GetDefaultVisual()
    {
        if (defaultChassisVisuals != null)
        {
            for (int i = 0; i < defaultChassisVisuals.Count; i++)
            {

                if (defaultChassisVisuals[i].CanThisBeUsed())
                {
                    string s = defaultChassisVisuals[i].visual;

                    if (actionManager.currentAction.actionType == ThoughtType.Reaction)
                    {
                        s = InsertNames(s, actionManager.currentAction.actors[0].beingName, actionManager.currentAction.targets[0].beingName, actionManager.currentAction.provoker.actors[0].beingName);
                        Debug.Log(s);
                    }
                    else
                    {
                        s = InsertNames(s, actionManager.currentAction.actors[0].beingName, actionManager.currentAction.targets[0].beingName, "no provoker");
                        Debug.Log(s);
                    }
                }
            }
        }
    }

    private void GetResolutionVisual()
    {
        if (resolutionVisuals != null)
        {
            for (int i = 0; i < resolutionVisuals.Count; i++)
            {
                if (resolutionVisuals[i].CanThisBeUsed())
                {
                    string s = resolutionVisuals[i].visual;

                    if (actionManager.currentAction.actionType == ThoughtType.Reaction)
                    {
                        s = InsertNames(s, actionManager.currentAction.actors[0].beingName, actionManager.currentAction.targets[0].beingName, actionManager.currentAction.provoker.actors[0].beingName);
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

    private string InsertNames(string s, string attacker, string target, string provoker)
    {
        s = s.Replace("ACTOR", attacker);
        s = s.Replace("TARGET", target);
        s = s.Replace("PROVOKER", provoker);

        return s;
    }

    private List<ChassisRule> SortListBySalience(List<ChassisRule> list)
    {
        List<ChassisRule> SortedList = list.OrderByDescending(o => o.conditions.Count).ToList(); 
        return SortedList;
    }


    public void VisualiseThought(List<Thought> LIST2)
    {
        //put better rules in later

        for (int i = 0; i < LIST2.Count; i++)
        {
            if (LIST2[0].ability.abilityType == AbilityType.PublicNormal)
            {
                return;
            }

            if (LIST2[0].ability.abilityType == AbilityType.Instant)
            {
                if (LIST2[0].ability.visual != null)
                {
                    Debug.Log(LIST2[0].ability.visual);
                }
                if (LIST2[0].ability.visual == null)
                {

                }
                
            }
        }
    }

    public void Visualise(Action a)
    {
        if (a.ability.visual !=null)
        {
            Debug.Log(a.ability.visual);
        }
        if (a.ability.visual == null)
        {
            GetDefaultVisual();
        }
    }

    public void VisualiseResolution()
    {
        GetResolutionVisual();
    }
   



		
	
}
