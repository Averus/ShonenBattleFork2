using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour {

    public Ability ability;


	// Use this for initialization
	public void Initialize(Ability ability) {

        this.ability = ability;
 
        if (gameObject.GetComponentInChildren<Text>() && this.ability != null)
        {
            gameObject.GetComponentInChildren<Text>().text = this.ability.abilityName;
        }
        
		
	}
	

}
