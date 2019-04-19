using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Action{

    public ThoughtType actionType;
    public float toHit; //a formula of dexterity, accuracy etc called in ActionManager
    public float reflex;
    //public float priority;
    public List<Being> actors;
    public Ability ability;
    public List<Being> targets;
    public Action provoker;


    public Action(ThoughtType actionType, float toHit, float reflex, List<Being> actors, Ability ability, List<Being> targets)
    {
        this.actionType = actionType;
        this.toHit = toHit;
        this.reflex = reflex;
        this.ability = ability;
        this.targets = targets;
        this.actors = actors;
    }
}
