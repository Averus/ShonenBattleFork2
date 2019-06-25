using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviours : MonoBehaviour {

    public Ability ability;
    public Being target;
    private string type = "";
    private PlayerSelectAbilities playerSelectAbilities;

	// Use this for initialization
	public void SetAsAbilityButton(ActionManager actionManager, PlayerSelectAbilities psa, Ability ability) {

        this.ability = ability;
        this.playerSelectAbilities = psa;
 
        if (gameObject.GetComponentInChildren<Text>() && this.ability != null)
        {
            gameObject.GetComponentInChildren<Text>().text = this.ability.abilityName;
            type = "ability";
        }
       
	}

    public void SetAsTargetButton(ActionManager actionManager, PlayerSelectAbilities psa, Being target)
    {
        this.target = target;
        this.playerSelectAbilities = psa;

        if (gameObject.GetComponentInChildren<Text>() && this.target != null)
        {
            gameObject.GetComponentInChildren<Text>().text = this.target.beingName;
            type = "target";
        }
        

    }

    public void ButtonClicked()
    {
        if (type == "")
        {
            Debug.Log("Error: Button has not been initialized to a type");
        }
        if (type == "ability")
        {
            Debug.Log("PRESSED " + ability.abilityName);
            playerSelectAbilities.ChooseAbility(ability);
            playerSelectAbilities.DisplayValidTargets(ability);
        }
        if (type == "target")
        {
            playerSelectAbilities.SetTarget(target);
        }


    }

}
