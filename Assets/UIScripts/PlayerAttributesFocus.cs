using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributesFocus : MonoBehaviour {

    ActionManager actionManager;
    Being being;
    List<GameObject> focusTargets; 
    public RectTransform content; // the game object that must parent buttons that are to be displayed in the menu

    public GameObject focusDisplay;//a gameObject that displays a target name and an int of attributed
    public Text beingNameText;

    //Populate the focus list within a given being
    private void PopulateFocusList()
    {
        ClearMenu();
        if (being.focus.Count == 0)
        {
            //Create a focus token to represent the parent being (the one who's opening this menu) and set the amount of focus to howevermuch the being has
            FocusToken focusToken = new FocusToken(being, being, being.GetResource("FOCUS").current);
            //Add it to the beings focus list so it's always the first/last
            being.focus.Add(focusToken);

            for (int i = 0; i < actionManager.LIST1.Count; i++)
            {
                if (actionManager.LIST1[i].being != being)
                {
                    FocusToken focusToken2 = new FocusToken(being, actionManager.LIST1[i].being, 0f);
                    being.focus.Add(focusToken2);
                }
            }

        }
    }

    //Display the focus in the menu
    private void DisplayCurrentFocus(Being b)
    {
        ClearMenu();

        if (being.focus.Count == 0)
        {
            PopulateFocusList();
        }

        RecoverFocus();

        if (being.focus.Count > 0)
        {
            for (int ii = 0; ii < being.focus.Count; ii++)
            {
                Debug.Log(focusDisplay.ToString());
                Debug.Log("being focus count is " + being.focus.Count);
                Debug.Log("ii is " + ii);
                //create a focus display UI object
                GameObject display = GameObject.Instantiate(focusDisplay, content);
                //set the UI object to that focus token
                display.GetComponent<FocusTokenDisplay>().Initialize(being.focus[ii]);
                //space each button out by height
                display.transform.position = new Vector3(display.transform.position.x, display.transform.position.y - (25 * ii), display.transform.position.z);
                //display.transform.position = new Vector3(0,0,0);
                //give the focus token display a reference to a focus token
                display.GetComponent<FocusTokenDisplay>().focusToken = being.focus[ii];
                //Add this focus token display object to the menu
                focusTargets.Add(display);
            }
        }

    }   
        


    private void ClearMenu()
    {
        if (focusTargets != null)
        {
            if (focusTargets.Count > 0)
            {
                for (int i = focusTargets.Count - 1; i > -1; i--)
                {
                    GameObject.Destroy(focusTargets[i]);
                }
            }

        }
    }

    //if the being had focus attributed on something which is now not in the game, recover that focus (and place it on Self, or defence)
    private void RecoverFocus()
    {
        if (being.focus.Count > 0)
        {

            float lostFocus = 0;
            bool matchFound = false;
            for (int i = being.focus.Count-1; i > -1; i--)
            {
                matchFound = false;
                for (int ii = 0; ii < actionManager.LIST1.Count; ii++)
                {
                    if (being.focus[i].GetFocusTarget() == actionManager.LIST1[ii].being)
                    {
                        matchFound = true;
                    }                    
                }
                if (matchFound == false)
                {
                    lostFocus += being.focus[i].GetAttibutedFocus();
                    being.focus.RemoveAt(i);
                }                
            }
            for (int i = 0; i < being.focus.Count; i++)
            {
                if (being.focus[i].GetFocusTarget() == being)
                {
                    being.focus[i].ModulateFocus(lostFocus);
                }
            }
        }
    }

    public void Initialize(ActionManager actionManager, Being being)
    {
        this.actionManager = actionManager;
        this.being = being;
        focusTargets = new List<GameObject>();

        DisplayCurrentFocus(being);

    }
}
