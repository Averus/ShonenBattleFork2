using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectAbilities : MonoBehaviour {

    ActionManager actionManager;
    Being being;
    ThoughtType thoughtType;
    float reflexSpeed;

    List<Thought> playerSelectedAbilities;

    public GameObject button;
    
	
	// Update is called once per frame
	void Update () {


		
	}

    private void DisplayUseableAbilities()
    {
        Transform content = transform.GetChild(0).GetChild(0);     

        for (int i = 0; i < being.useableAbilities.Count; i++)
        {
            //put a button in the list
            GameObject b = GameObject.Instantiate(button, content);
            //space each button out by height
            b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y - (25 * i), b.transform.position.z);
            //give the button a reference to this ability
            b.GetComponent<ButtonBehaviours>().SetAsAbilityButton(actionManager, this, being.useableAbilities[i]);

            //Set the buttons onClick behaviour to fire its own ButtonClicked method from its own ButtonBehaviours script (because each button is initialized differently)
            //This onClick behaviour does not show in the inspector for some reason, but it does work
            b.GetComponent<Button>().onClick.AddListener(b.GetComponent<ButtonBehaviours>().ButtonClicked);
        }
    }

    public void DisplayValidTargets(Ability ability)
    {
        Debug.Log("displaying targets for " + ability.abilityName);
    }


    public void Initialize(ActionManager actionManager, Being being, ThoughtType thoughtType)
    {
        this.actionManager = actionManager;
        this.being = being;
        this.thoughtType = thoughtType;

        playerSelectedAbilities = new List<Thought>();

        DisplayUseableAbilities();
   

    }

    public void Initialize(ActionManager actionManager, Being being, ThoughtType thoughtType, float reflexSpeed)
    {
        this.reflexSpeed = reflexSpeed;
        Initialize(actionManager, being, thoughtType);

    }
}
