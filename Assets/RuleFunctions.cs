using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RuleFunctions : ScriptableObject
{

    public void BasicResolutionChecks(Being b)
    {
        DamageCheck(b);
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
                //Debug.Log(damage + " hp added to " + b.beingName);
            }

            hp.last = hp.GetCurrent();
        }
    }

}
