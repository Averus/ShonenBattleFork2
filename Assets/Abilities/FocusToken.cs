using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusToken {

    //This class holds a target and an amount of focus attributed to it.
    private Being parentBeing;
    private Being target;
    private float attributedFocus;

    public FocusToken(Being parentBeing, Being target, float attributedFocus)
    {
        this.parentBeing = parentBeing;
        this.target = target;
        this.attributedFocus = attributedFocus;
    }

    public void BeingIncrementsFocus(Being focusTarget)
    {
        Debug.Log("parent being is " + parentBeing.beingName);
        //if the being has enough focus stored in the token that represents focus on itself (its defence token)
        if (GetFocusToken(parentBeing, parentBeing).GetAttibutedFocus() > 0)
        {
            //Decrement the beings defence focus
            GetFocusToken(parentBeing, parentBeing).DecrementFocus();
            //increase the focus this token is pointing at instead
            IncrementFocus();
        }
    }

    public void BeingDecrementsFocus(Being focusTarget)
    {
        //if the being has enough focus stored in this token
        if (attributedFocus > 0)
        {
            //Decrement this tokens count
            DecrementFocus();
            //increase the focus pointed at the being itself
            GetFocusToken(parentBeing, parentBeing).IncrementFocus();
        }
    }


    private FocusToken GetFocusToken(Being focusTarget, Being FocusOwner)
    {
        FocusToken ft = new FocusToken(parentBeing, parentBeing, -1f);

        for (int i = 0; i < FocusOwner.focus.Count; i++)
        {
            if (FocusOwner.focus[i].target == focusTarget)
            {
                ft = FocusOwner.focus[i];
            }
        }

        return ft;
    }

    public float GetAttibutedFocus()
    {
        return attributedFocus;
    }
    public Being GetFocusTarget()
    {
        return target;
    }

    private void IncrementFocus()
    {
        attributedFocus++;
    }
    private void DecrementFocus()
    {
        attributedFocus--;
    }

    public void ModulateFocus(float amount)
    {
        attributedFocus += amount;
    }
}
