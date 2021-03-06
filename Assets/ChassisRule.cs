﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChassisRule {

    public string visual = "NO VISUAL";

    public List<ChassisCondition> conditions = new List<ChassisCondition>();
    public List<FunctionCall> functons = new List<FunctionCall>();

    public bool CanThisBeUsed()
    {
        if (conditions != null)
        {
            for (int i = 0; i < conditions.Count; i++)
            {
                if (conditions[i].CanThisBeUsed() == false)
                {
                    // Debug.Log("Rule " + i.ToString() + "returns false;");
                    return false;
                }
            }
            return true;
        }
        Debug.Log("Error: Chassis rule has no conditions");
        return false;
    }

}
