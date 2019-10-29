using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RuleFunctions : ScriptableObject
{
    public GameObject attributeFocusMenu;

    //Calls the focus attribution menu. This function is called by an 'attribute focus' ability that all beings who can do so should have
    public void PlayerAttributesFocus(ActionManager actionManager, Transform canvas, Being being) 
    {
        GameObject ob = GameObject.Instantiate(attributeFocusMenu, canvas);
        ob.GetComponent<PlayerAttributesFocus>().Initialize(actionManager, being);
    }

    public void BasicResolutionChecks(Being b)
    {
        DamageCheck(b);
        ToughnessVsDamage(b);
    }

    void DamageCheck(Being b)
    {
        if (b.GetResource("HP") == null)
        {
            Debug.Log("Damage check cannot find an HP resource in Being " + b.beingName);
        }
        else
        {
            Resource hp = b.GetResource("HP");

            if (hp.GetCurrent() <= hp.last)
            {
                float damage = hp.last - hp.GetCurrent();
                b.HPDamageThisTurn = damage;
                //Debug.Log(damage + " damage dealt to " + b.beingName);
            }
            else
            {
                float damage = hp.GetCurrent() - hp.last;
                b.HPDamageThisTurn = damage;
               // Debug.Log(damage + " hp added to " + b.beingName);
            }

            hp.last = hp.GetCurrent();
        }
    }

    void ToughnessVsDamage(Being b)
    {
        if (b.GetStat("TOUGHNESS") == null)
        {
            Debug.Log("Cannot find an TOUGHNESS resource in Being " + b.beingName);
        }
        else
        {
            Stat toughness = b.GetStat("TOUGHNESS");

            if (b.HPDamageThisTurn > toughness.current)
            {
                Debug.Log(b.beingName + " is staggered");
                b.status = Being.Status.staggered;
            }
        }
    }


}
