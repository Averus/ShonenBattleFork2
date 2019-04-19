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
    public List<Being> actors;
    public readonly Ability ability;
    public readonly List<Being> targets = new List<Being>();
    public Action provoker; //if this is a reaction, this is the action it's reacting to. It's set in Being.React();


    public Thought(ThoughtType thoughtType, float reflex, Ability ability, List<Being> targets)
    {
        this.thoughtType = thoughtType;
        this.reflex = reflex;
        this.ability = ability;
        this.targets.AddRange(targets);
        this.actors = new List<Being>();
    }

}
