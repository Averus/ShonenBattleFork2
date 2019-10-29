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
    List<GameObject> buttons; //The buttons currently displayed
    Transform content; // the game object that must parent buttons that are to be displayed in the menu

    public GameObject button;
    public Text beingNameText;


    private void DisplayUseableAbilities()
    {
        ClearMenu();
        being.ListUseableAbilities();

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
            buttons.Add(b);
        }
    }

    private void ClearMenu()
    {
        if (buttons != null)
        {
            if (buttons.Count > 0)
            {
                for (int i = buttons.Count -1; i > -1; i--)
                {
                    GameObject.Destroy(buttons[i]);
                }
            }

        }
    }

    public void DisplayValidTargets(Ability ability)
    {
        ClearMenu();

        for (int i = 0; i < ability.validTargets.Count; i++)
        {
            //put a button in the list
            GameObject b = GameObject.Instantiate(button, content);
            //space each button out by height
            b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y - (25 * i), b.transform.position.z);
            //give the button a reference to a target
            b.GetComponent<ButtonBehaviours>().SetAsTargetButton(actionManager, this, ability.validTargets[i]);
            //Set the buttons onClick behaviour to fire its own ButtonClicked method from its own ButtonBehaviours script (because each button is initialized differently)
            //This onClick behaviour does not show in the inspector for some reason, but it does work
            b.GetComponent<Button>().onClick.AddListener(b.GetComponent<ButtonBehaviours>().ButtonClicked);
            buttons.Add(b);
        }
    }

    //called from an ability button, creates a thought containing the selected ability and puts it in the playerSelectedAbility list
    public void ChooseAbility(Ability ability)
    {
        List<Being> targets = new List<Being>();

        if (reflexSpeed == 0)
        {
            Thought thought = new Thought(thoughtType, being.rollReflex(), ability, targets);
            thought.actors.Add(being);
            playerSelectedAbilities.Add(thought);
            return;
        }
        else
        {
            Thought thought = new Thought(thoughtType, reflexSpeed, ability, targets);
            thought.actors.Add(being);
            playerSelectedAbilities.Add(thought);
        }

    }

    //This needs ability because a player can choose multiple abilities on their turn (from this one menu?)
    public void SetTarget(Being target)
    {

        if (playerSelectedAbilities[playerSelectedAbilities.Count - 1].targets.Count < playerSelectedAbilities[playerSelectedAbilities.Count - 1].ability.numberOfTargets)
        {
            playerSelectedAbilities[playerSelectedAbilities.Count - 1].targets.Add(target);
        }
        else
        {
            //check to see if we're ready to return to ActionManager
            //Do we have one and only one public normal etc (perhaps check that in 'choose ability' or rebuild the buttons after one has been chosen)
            for (int i = 0; i < playerSelectedAbilities.Count; i++)
            {
                if (playerSelectedAbilities[i].ability.abilityType == AbilityType.PublicNormal) 
                {
                    actionManager.ProposeActions(playerSelectedAbilities);
                    GameObject.Destroy(gameObject);
                    return;
                }

                DisplayUseableAbilities();

            }

            
        }

       
    }

    public void Initialize(ActionManager actionManager, Being being, ThoughtType thoughtType)
    {
        this.actionManager = actionManager;
        this.being = being;
        this.thoughtType = thoughtType;

        this.beingNameText.text = being.beingName;
        this.playerSelectedAbilities = new List<Thought>();
        this.buttons = new List<GameObject>();
        this.content = transform.GetChild(0).GetChild(0);

        DisplayUseableAbilities();
   

    }

    public void Initialize(ActionManager actionManager, Being being, ThoughtType thoughtType, float reflexSpeed)
    {
        this.reflexSpeed = reflexSpeed;
        Initialize(actionManager, being, thoughtType);

    }
}
