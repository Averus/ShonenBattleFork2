using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Canvas canvas;
    public GameObject beingInfoPrefab;
    public ActionManager actionManager;

	// Use this for initialization
	void Start () {

        actionManager = GameObject.Find("ActionManager").GetComponent<ActionManager>();
		
	}

    public void DisplayCombatantInfo(List<Being> b)
    {
        Debug.Log("Displaying combatant info");

        for (int i = 0; i < b.Count; i++)
        {
            GameObject g = GameObject.Instantiate(beingInfoPrefab, canvas.transform);
            g.transform.position = g.transform.position += new Vector3(i * 210,0,0);
            g.GetComponent<DisplayBeingInfo>().being = b[i];
                
               
        }

       

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
