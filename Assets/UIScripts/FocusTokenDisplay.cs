using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusTokenDisplay : MonoBehaviour {

    public FocusToken focusToken;
    public Text textOb;
    public Text valueOb;

    public void PlusButton()
    {
        Debug.Log(focusToken.GetFocusTarget().beingName);
        focusToken.BeingIncrementsFocus(focusToken.GetFocusTarget());
    }
    public void MinusButton()
    {
        focusToken.BeingDecrementsFocus(focusToken.GetFocusTarget());
    }

    public void Initialize(FocusToken focusToken)
    {
        this.focusToken = focusToken;
        textOb.text = focusToken.GetFocusTarget().beingName;
        valueOb.text = "" + focusToken.GetAttibutedFocus();
    }

    public void Update()
    {
        valueOb.text = "" + focusToken.GetAttibutedFocus();
    }

}
