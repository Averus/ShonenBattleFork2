using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VisualManager : ScriptableObject {



    public void VisualiseThought(List<Thought> LIST2)
    {
        //put better rules in later

        for (int i = 0; i < LIST2.Count; i++)
        {
            if (LIST2[0].ability.abilityType == AbilityType.PublicNormal)
            {
                return;
            }

            if (LIST2[0].ability.abilityType == AbilityType.Instant)
            {
                if (LIST2[0].ability.visual != null)
                {
                    Debug.Log(LIST2[0].ability.visual);
                }
                if (LIST2[0].ability.visual == null)
                {

                }
                
            }
        }
    }


    public void VisualiseAction(Action a)
    {
        if (a.ability.visual !=null)
        {
            Debug.Log(a.ability.visual);
        }
        if (a.ability.visual == null)
        {
            DefaultVisual(a);
        }
    }

    void DefaultVisual(Action a)
    {

    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
