using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_EffectsInPlayViewer : MonoBehaviour {

    public GameObject ActionManager;
    private ActionManager actionManager;

    Text text;

	// Use this for initialization
	void Start () {

        text = GetComponent<Text>();

        actionManager = ActionManager.GetComponent<ActionManager>();

		
	}
	
	// Update is called once per frame
	void Update () {

        if (actionManager != null)
        {
            if (actionManager.effectsInPlay.Count > 0)
            {
                string s = "Effects in play";
                s+= "\n";

                for (int i = 0; i < actionManager.effectsInPlay.Count; i++)
                {
                    s += actionManager.effectsInPlay[i].effect.effectName + " on " + actionManager.effectsInPlay[i].targets[0].beingName;
                    s+= "\n";
                }

                text.text = s;
            }
        }
		
	}
}
