using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge_DefenceEffect : Effect
{
    public override void Use(Being target)
    {
        Debug.Log(target.beingName + " flickers as they dodge with super speed.");

    }






    public Dodge_DefenceEffect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName) : base(actionManager, parentBeing, parentAbility, effectName)
    {

    }


}
