using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_Visualiser : MonoBehaviour {

    public Text text;
    public List<string> debugVisuals = new List<string>();

    // Use this for initialization
    void Start()
    {

        text = GetComponent<Text>();

    }

    public void Debug_DisplayNewVisuals(List<string> visuals)
    {

        debugVisuals = visuals;
    }

    // Update is called once per frame
    void Update () {

        if (debugVisuals.Count > 0)
        {
            string s = "";

            for (int i = 0; i < debugVisuals.Count; i++)
            {
                s += debugVisuals[i] + "\n";
            }

            text.text = s;
        }
		
	}
}
