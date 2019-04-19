using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_LISTviewer : MonoBehaviour {


    public ActionManager actionmanager;
    public Text text;

	// Use this for initialization
	void Start () {

        text = GetComponent<Text>();
        
    }
	
	// Update is called once per frame
	void Update () {


        string t = "LIST1" + "\n";

        

        for (int i = 0; i < actionmanager.LIST1.Count; i++)
        {
            t += actionmanager.LIST1[i].being.beingName + " Reflex: " + actionmanager.LIST1[i].reflexSpeed + "\n";
        }

        t += "\n";
        t += "LIST2";
        t += "\n";


        for (int i = 0; i < actionmanager.LIST2.Count; i++)
        {
            if (actionmanager.LIST2[i].thoughtType == ThoughtType.Reaction)
            {
                t += actionmanager.LIST2[i].thoughtType + " " + actionmanager.LIST2[i].provoker.toHit + " " + actionmanager.LIST2[i].ability.abilityType + " " + actionmanager.LIST2[i].ability.GetParentBeing().beingName + ": " + actionmanager.LIST2[i].ability.abilityName + "-->" + actionmanager.LIST2[i].targets[0].beingName + " Reflex: " + actionmanager.LIST2[i].reflex + "\n";
            }
            else
            {
                t += actionmanager.LIST2[i].thoughtType + " " + actionmanager.LIST2[i].ability.abilityType + " " + actionmanager.LIST2[i].ability.GetParentBeing().beingName + ": " + actionmanager.LIST2[i].ability.abilityName + "-->" + actionmanager.LIST2[i].targets.Count + " Reflex: " + actionmanager.LIST2[i].reflex + "\n";
            }

        }

        t += "\n";
        t += "LIST3";
        t += "\n";

        for (int i = 0; i < actionmanager.LIST3.Count; i++)
        {
            if (actionmanager.LIST3[i].actionType == ThoughtType.Reaction)
            {
                t += actionmanager.LIST3[i].actionType + " " + actionmanager.LIST3[i].provoker.toHit + " " + actionmanager.LIST3[i].ability.abilityType + " " + actionmanager.LIST3[i].ability.GetParentBeing().beingName + ": " + actionmanager.LIST3[i].ability.abilityName + "-->" + actionmanager.LIST3[i].targets[0].beingName + " Reflex: " + actionmanager.LIST3[i].reflex + " ToHit: " + actionmanager.LIST3[i].toHit + "\n";
            }
            else
            {
                t += actionmanager.LIST3[i].actionType + " " + actionmanager.LIST3[i].ability.abilityType + " " + actionmanager.LIST3[i].ability.GetParentBeing().beingName + ": " + actionmanager.LIST3[i].ability.abilityName + "-->" + actionmanager.LIST3[i].targets[0].beingName + " Reflex: " + actionmanager.LIST3[i].reflex + " ToHit: " + actionmanager.LIST3[i].toHit + "\n";
            }
            
        }

    

        text.text = t;
    }
}
