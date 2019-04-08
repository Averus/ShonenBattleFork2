﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulateResource_Effect : Effect{

    public string targetResource;
    public int value;
    public bool targetSelf;


    public override void Use(Being target)
    {

        Resource actualTargetResource;

        if (targetSelf)
        {
            actualTargetResource = parentBeing.GetResource(targetResource);
            ResourceModifier rm = new ResourceModifier(value);
            actualTargetResource.Modify(rm);
        }

        if (!targetSelf)
        {
           actualTargetResource = target.GetResource(targetResource);
           ResourceModifier rm = new ResourceModifier(value);
           actualTargetResource.Modify(rm);
        }

        

    }



    public ModulateResource_Effect(ActionManager actionManager, Being parentBeing, Ability parentAbility, string effectName, string targetStat, int value, bool targetSelf, CombatState usedInState) : base(actionManager, parentBeing, parentAbility, effectName, usedInState)
    {
        this.targetResource = targetStat;
        this.value = value;
        this.targetSelf = targetSelf;
    }
}
