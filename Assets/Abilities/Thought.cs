using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ThoughtType
{
    Normal = 100,
    Reaction = 200,
}

[System.Serializable]
public class Thought{

    public ThoughtType thoughtType;
    public float reflex;
    public float priority;
    public List<Being> actors;
    public readonly Ability ability;
    public readonly List<Being> targets;
    public Action provoker; //if this is a reaction, this is the action it's reacting to. It's set in Being.React();


    public Thought(ThoughtType thoughtType, float reflex, float priority, Ability ability)
    {
        this.thoughtType = thoughtType;
        this.reflex = reflex;
        this.priority = priority;
        this.ability = ability;
        this.targets = new List<Being>();
        this.actors = new List<Being>();
    }

}
