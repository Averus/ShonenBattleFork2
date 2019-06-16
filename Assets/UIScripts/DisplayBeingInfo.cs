using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBeingInfo : MonoBehaviour {


    public Being being;
    Text text;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();

	}
	
    private string GetResources()
    {
        string s = "";

        for (int i = 0; i < being.resources.Count; i++)
        {
            s += being.resources[i].resourceName + " " + being.resources[i].GetCurrent() + "/" + being.resources[i].GetMax() + "\n";
        }

        return s;
    }

    private string GetStats()
    {
        string s = "";

        for (int i = 0; i < being.stats.Count; i++)
        {
            s += being.stats[i].statName + " " + being.stats[i].current + "/" + being.stats[i].max + "\n";
        }

        return s;
    }

	// Update is called once per frame
	void Update () {

        text.text =
            "Name: " + being.beingName + "\n" +
            "\n" +
            "Resources " + "\n" +
            GetResources() +
            "\n" +
            "Stats " + "\n" +
            GetStats() +
            "\n";






    }
}
