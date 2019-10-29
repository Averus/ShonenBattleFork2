using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeingFactory : MonoBehaviour {

    public ActionManager actionManager;
    public GameObject beingPrefab;
    public RuleFunctions ruleFunctions;


    // Use this for initialization
    void Start()
    {

        actionManager = GetComponent<ActionManager>();

        if (beingPrefab == null)
        {
            Debug.Log("ERROR: Beingfactory requires beingPrefab reference");
        }



    }

    //Resources
    void BasicResources(Being b)
    {
        Resource hp = new Resource("HP", 100, 100);
        Resource stamina = new Resource("STAMINA", 100, 100);
        Resource powerLevel = new Resource("POWERLEVEL", 10);
        Resource focus = new Resource("FOCUS", 10);

        b.resources.Add(powerLevel);
        b.resources.Add(hp);
        b.resources.Add(stamina);
        b.resources.Add(focus);
    }
    //stat blocks
    void BasicStats(Being b)
    {



    }
    void NewBasicStats(Being b)
    {
        Stat reflex = new Stat("REFLEX", b, 100, 100);
        b.stats.Add(reflex);

        Stat dex = new Stat("DEXTERITY", b, 50, 100);
        b.stats.Add(dex);

        Stat tou = new Stat("TOUGHNESS", b, 10, 100);
        b.stats.Add(tou);
    }
    //defence blocks
    /*
    void BasicDefences(Being b) 
    {
        Ability def1 = new Ability(b,"Block",AbilityChassis.Block, AbilityType.PublicNormal, 100, 1, true);
        def1.isDefence = true;
        def1.defenceSpeed = 50;
        ResourceAtValue_Condition reqStam = new ResourceAtValue_Condition(actionManager, b, "Stamina above 0", "STAMINA", ">", 0);
        ModulateResource_Effect costsStamina = new ModulateResource_Effect(actionManager, b, def1, "CostsStamina", "STAMINA", -10, true, CombatState.Activate);
        Block_DefenceEffect block = new Block_DefenceEffect(actionManager, b, def1, "Block", CombatState.Hit);
        Self_TargetingCriteria self = new Self_TargetingCriteria(actionManager, b);

        def1.effects.Add(block);
        def1.effects.Add(costsStamina);
        def1.conditions.Add(reqStam);
        def1.targetingCriteria.Add(self);
        b.defences.Add(def1);

        //Create a generic 'dodge' defence
        Ability def2 = new Ability(b, "Dodge", AbilityChassis.Dodge, AbilityType.PublicNormal, 100, 1, true);
        def2.isDefence = true;
        def2.defenceSpeed = 40;
        ResourceAtValue_Condition reqStam2 = new ResourceAtValue_Condition(actionManager, b, "Stamina above 0", "STAMINA", ">", 0);
        ModulateResource_Effect costsStamina2 = new ModulateResource_Effect(actionManager, b, def2, "CostsStamina", "STAMINA", -20, true, CombatState.Activate);
        Dodge_DefenceEffect dodge = new Dodge_DefenceEffect(actionManager, b, def2, "Dodge", CombatState.Hit);
        Self_TargetingCriteria self2 = new Self_TargetingCriteria(actionManager, b);

        def2.effects.Add(dodge);
        def2.effects.Add(costsStamina2);
        def2.conditions.Add(reqStam2);
        def2.targetingCriteria.Add(self2);
        b.defences.Add(def2);

        //Create a generic 'free action' defence
        //Ability def3 = new Ability(b, "Any Action", 100, 1, true);
       // def3.isDefence = true;
       // def3.defenceSpeed = 5;
        NoCondition_Condition noCondition6 = new NoCondition_Condition(actionManager, b, "NoCondition");
      //  Self_TargetingCriteria self3 = new Self_TargetingCriteria(actionManager, b, def3);
      //  AnyAction_DefenceEffect anyAction = new AnyAction_DefenceEffect(actionManager, b, def3, "AnyAction");

       // def3.effects.Add(anyAction);
       // def3.conditions.Add(noCondition6);
       // def3.targetingCriteria.Add(self3);
       // b.defences.Add(def3);
    }
    */
    //behaviours
    void BasicBeingBehaviours(Being b)
    {
        //These behaviours will do things like select for attributing focus
        Behaviour attributeFocus = new Behaviour(actionManager, b, "Attribute Focus", ThoughtType.Normal);
        IsBeingsTurn_Condition isBeingsTurn = new IsBeingsTurn_Condition(actionManager, b, "Is my turn", b);
        IncludesEffect_SelectionCriteria includesAttributeFocus = new IncludesEffect_SelectionCriteria(actionManager, b, "Attribute Focus", "Attribute Focus");

        attributeFocus.conditions.Add(isBeingsTurn);
        attributeFocus.selectionCriteria.Add(includesAttributeFocus);
        b.behaviours.Add(attributeFocus);
    }
    void AttackBehaviour(Being b)
    {
        //Create a new behaviour called 'just attack'
        Behaviour justAttack = new Behaviour(actionManager, b, "Just attack", ThoughtType.Normal);
        NoCondition_Condition noCondition = new NoCondition_Condition(actionManager, b, "NoCondition");
        IncludesEffect_SelectionCriteria includesDamage = new IncludesEffect_SelectionCriteria(actionManager, b, "IncludesDamage", "Damage");

        justAttack.conditions.Add(noCondition); ;
        justAttack.selectionCriteria.Add(includesDamage);
        b.behaviours.Add(justAttack);
    }
    void HealSelfBehaviour(Being b)
    {
        //Create a new behaviour called 'heal self'
        Behaviour healSelf = new Behaviour(actionManager, b, "Heal self", ThoughtType.Normal);
        //Create a condition that hp must be below 20 
        ResourceAtValue_Condition hpLessThanTwenty = new ResourceAtValue_Condition(actionManager, b, "HP less that 20", "HP", "<", 20);
        //Create a selectionCriteria for appropriate abilities that the must have a 'healself' effect
        IncludesEffect_SelectionCriteria includesHealSelf = new IncludesEffect_SelectionCriteria(actionManager, b, "IncludesHealSelf", "HealSelf");
        Self_TargetingCriteria targetSelf = new Self_TargetingCriteria(actionManager, b);

        healSelf.conditions.Add(hpLessThanTwenty); ;
        healSelf.selectionCriteria.Add(includesHealSelf);
        healSelf.targetingCriteria.Add(targetSelf);
        b.behaviours.Add(healSelf);
    }
    public void AttackTeamBehaviour(Being b, int team)
    {
        //Create a new behaviour called 'just attack'
        Behaviour attackTeam = new Behaviour(actionManager, b, "Attack Team", ThoughtType.Normal);
        NoCondition_Condition noCondition = new NoCondition_Condition(actionManager, b, "NoCondition");
        IncludesEffect_SelectionCriteria includesDamage = new IncludesEffect_SelectionCriteria(actionManager, b, "IncludesDamage", "Damage");
        OnTeam_TargetingCriteria onTeam = new OnTeam_TargetingCriteria(actionManager, b, team);

        attackTeam.conditions.Add(noCondition); ;
        attackTeam.selectionCriteria.Add(includesDamage);
        attackTeam.targetingCriteria.Add(onTeam);
        b.behaviours.Add(attackTeam);
    }
    //Reactions (behaviours with an action parameter)
    void BasicReactions(Being b)
    {
        //Create a new reaction called 'self preservation'
        Reaction selfPreservation = new Reaction(actionManager, b, "Self Preservation",ThoughtType.Reaction);
        AmTarget_ReactionCondition amtarget = new AmTarget_ReactionCondition(actionManager, b, "Am target");
        ContainsEffect_ReactionCondition dealsDamage = new ContainsEffect_ReactionCondition(actionManager, b, "Deals Damage", "Damage");
        IncludesEffect_SelectionCriteria dodges = new IncludesEffect_SelectionCriteria(actionManager, b, "dodges", "Dodge");
        IncludesEffect_SelectionCriteria blocks = new IncludesEffect_SelectionCriteria(actionManager, b, "blocks", "Block");
        Self_TargetingCriteria targetSelf = new Self_TargetingCriteria(actionManager, b);
        

        selfPreservation.reactionConditions.Add(amtarget);
        selfPreservation.reactionConditions.Add(dealsDamage);
        selfPreservation.selectionCriteria.Add(dodges);
        selfPreservation.selectionCriteria.Add(blocks);
        selfPreservation.targetingCriteria.Add(targetSelf);

        b.reactions.Add(selfPreservation);

        //create a reaction called 'notice danger'
        Reaction noticeDanger = new Reaction(actionManager, b, "Notice Danger", ThoughtType.Reaction);
        ContainsEffect_ReactionCondition dealsDamage2 = new ContainsEffect_ReactionCondition(actionManager, b, "Deals Damage", "Damage");
        IncludesEffect_SelectionCriteria exclamation = new IncludesEffect_SelectionCriteria(actionManager, b, "Exclamation", "Exclamation");
        Self_TargetingCriteria targetSelf2 = new Self_TargetingCriteria(actionManager, b);

        noticeDanger.reactionConditions.Add(dealsDamage2);
        noticeDanger.selectionCriteria.Add(exclamation);
        noticeDanger.targetingCriteria.Add(targetSelf2);

        b.reactions.Add(noticeDanger);

    }
    public void DefendTeamReaction(Being b, int team)
    {
        //Create a new reaction called 'self preservation'
        Reaction defendTeam = new Reaction(actionManager, b, "Defend Team", ThoughtType.Reaction);
        NoCondition_Condition noCondition = new NoCondition_Condition(actionManager, b, "No Condition");
        TargetOnTeam_ReactionCondition targetOnTeam = new TargetOnTeam_ReactionCondition(actionManager, b, "target on team", team);
        ContainsEffect_ReactionCondition dealsDamage = new ContainsEffect_ReactionCondition(actionManager, b, "Deals Damage", "Damage");
        IncludesEffect_SelectionCriteria blocks = new IncludesEffect_SelectionCriteria(actionManager, b, "blocks", "Block");
        CurrentActionActor_TargetingCriteria currentActionActor = new CurrentActionActor_TargetingCriteria(actionManager, b);

        defendTeam.conditions.Add(noCondition);
        defendTeam.reactionConditions.Add(targetOnTeam);
        defendTeam.reactionConditions.Add(dealsDamage);
        defendTeam.selectionCriteria.Add(blocks);
        defendTeam.targetingCriteria.Add(currentActionActor);

        b.reactions.Add(defendTeam);
    }
    //ability packs
    void BasicBeingAbilities(Being b)
    {
        //This pack is for things like being able to attribute focus (that's not passive because you need to be concious to do it, unlike reganing stamina which skirts close to being a rule rather than an 'ability')

        Ability a = new Ability(b, "Attribute Focus", AbilityChassis.Thought, AbilityType.Instant, 100, 1, false);
        a.isDefence = false;

        NewRound_Condition nrc = new NewRound_Condition(actionManager, b, "New round condition");

        AttributeFocus_Effect attributeFocus_Effect = new AttributeFocus_Effect(actionManager, b, a, "Attribute Focus", CombatState.Activate, 0, 0);
        Self_TargetingCriteria self = new Self_TargetingCriteria(actionManager, b);

        a.effects.Add(attributeFocus_Effect);
        a.targetingCriteria.Add(self);
        b.abilities.Add(a);
    }
    void BasicAttackAbilities(Being b)
    {

        Ability ab3 = new Ability(b, "Echoing Punch", AbilityChassis.Melee, AbilityType.PublicNormal, 100, 1, false);
        StatusIsNot_Condition notStaggered = new StatusIsNot_Condition(actionManager, b, "IsNotStaggered", Being.Status.staggered);
        ResourceAtValue_Condition reqStam1 = new ResourceAtValue_Condition(actionManager, b, "Stamina above 0", "STAMINA", ">", 0);
        ModulateResource_Effect costsStamina1 = new ModulateResource_Effect(actionManager, b, ab3, "CostsStamina", "STAMINA", -20, true, CombatState.Activate, 0, 0);
        Damage_Effect damage = new Damage_Effect(actionManager, b, ab3, "Damage", "HP", 1, CombatState.Hit,0,0);
        Others_TargetingCriteria o = new Others_TargetingCriteria(actionManager, b);

        ab3.conditions.Add(notStaggered);
        ab3.conditions.Add(reqStam1);
        ab3.effects.Add(costsStamina1);
        ab3.effects.Add(damage);
        ab3.targetingCriteria.Add(o);
        ab3.numberOfTargets = 1;
        b.abilities.Add(ab3);


        Ability ab4 = new Ability(b, "Punch", AbilityChassis.Melee,AbilityType.PublicNormal, 100,1, false);
        StatusIsNot_Condition notStaggered2 = new StatusIsNot_Condition(actionManager, b, "NoCondition", Being.Status.staggered);
        ResourceAtValue_Condition reqStam = new ResourceAtValue_Condition(actionManager, b, "Stamina above 0", "STAMINA", ">", 0);
        ModulateResource_Effect costsStamina = new ModulateResource_Effect(actionManager, b, ab3, "CostsStamina", "STAMINA", -20, true, CombatState.Activate,0,0);
        Damage_Effect damage2 = new Damage_Effect(actionManager, b, ab3, "Damage", "HP", 2, CombatState.Hit,0,0);
        Others_TargetingCriteria o2 = new Others_TargetingCriteria(actionManager, b);

        ab4.abilityType = AbilityType.PublicNormal;
        ab4.conditions.Add(notStaggered2);
        //ab4.conditions.Add(reqStam);
        ab4.effects.Add(costsStamina);
        ab4.effects.Add(damage2);
        ab4.targetingCriteria.Add(o2);
        ab4.numberOfTargets = 1;
        b.abilities.Add(ab4);

        Ability overcome = new Ability(b, "Overcome Stagger", AbilityChassis.StaggerRecover, AbilityType.PublicNormal, 100, 1, false);
        StatusIs_Condition isStaggered = new StatusIs_Condition(actionManager, b, "Staggered", Being.Status.staggered);
        ResourceAtValue_Condition reqStam2 = new ResourceAtValue_Condition(actionManager, b, "Stamina above 0", "STAMINA", ">", 0);
        Self_TargetingCriteria selftarget = new Self_TargetingCriteria(actionManager, b);

        overcome.conditions.Add(isStaggered);
        overcome.conditions.Add(reqStam2);
        overcome.targetingCriteria.Add(selftarget);
        b.abilities.Add(overcome);





    }
    void BasicDefenceAbilities(Being b)
    {
        Ability dodge = new Ability(b, "Dodge", AbilityChassis.Dodge, AbilityType.PublicNormal, 100, 1, false);
        StatusIsNot_Condition notStaggered = new StatusIsNot_Condition(actionManager, b, "NoCondition", Being.Status.staggered);
        Dodge_DefenceEffect dodgeEffect = new Dodge_DefenceEffect(actionManager, b, dodge, "Dodge", CombatState.Hit,0,0);
        Others_TargetingCriteria others = new Others_TargetingCriteria(actionManager, b);

        dodge.conditions.Add(notStaggered);
        dodge.effects.Add(dodgeEffect);
        dodge.targetingCriteria.Add(others);

        b.abilities.Add(dodge);

        Ability block = new Ability(b, "Block", AbilityChassis.Block, AbilityType.PublicNormal, 100, 1, false);
        StatusIsNot_Condition notStaggered2 = new StatusIsNot_Condition(actionManager, b, "NoCondition", Being.Status.staggered);
        Block_DefenceEffect block2 = new Block_DefenceEffect(actionManager, b, block, "Block", CombatState.Hit,0,0);
        Others_TargetingCriteria others2 = new Others_TargetingCriteria(actionManager, b);

        block.conditions.Add(notStaggered2);
        block.effects.Add(block2);
        block.targetingCriteria.Add(others2);

        b.abilities.Add(block);


    }
    void BasicSelfHealingAbility(Being b)
    {
        //Create a new ability called Healself
        Ability ab = new Ability(b, "Heal", AbilityChassis.Melee, AbilityType.PublicNormal, 100, 1, false);
        //create a condition that MP must not be greater the 5 (the cost)
        ResourceAtValue_Condition gt = new ResourceAtValue_Condition(actionManager, b, "MP greater than 5", "MP", ">", 5);

        HealSelf_Effect hs = new HealSelf_Effect(actionManager, b, ab, "HealSelf", 10, CombatState.Hit,0,0);
        Self_TargetingCriteria s = new Self_TargetingCriteria(actionManager, b);

        ab.conditions.Add(gt);
        ab.effects.Add(hs);
        ab.targetingCriteria.Add(s);
        b.abilities.Add(ab);


    }
    //passive ability packs
    void BasicPassiveAbilities(Being b)
    {
        Ability reg = new Ability(b, "Stamina regen", AbilityChassis.Block, AbilityType.PublicNormal, 100, 1, true);
        reg.isDefence = false;

        //NewRound_Condition nrc = new NewRound_Condition(actionManager, b, "New round condition");
        ModulateResource_Effect staminaRegen = new ModulateResource_Effect(actionManager, b, reg, "Stamina regen", "STAMINA", +50, true, CombatState.Activate,0,0);
        Self_TargetingCriteria self = new Self_TargetingCriteria(actionManager, b);

        reg.effects.Add(staminaRegen);
        //reg.conditions.Add(nrc);
        reg.targetingCriteria.Add(self);
        b.abilities.Add(reg);
    }
    
   
    public Being CreateBeing(string name)
    {

        GameObject g = Instantiate(beingPrefab);
        Being p;

        if(g.GetComponent<Being>() == false)
        {
            Debug.Log("ERROR: beingfactory's references beingPrefab has no Being component");
            return null;
        }

        p = g.GetComponent<Being>();

        //Name being
        p.beingName = name;


        //Bestow Resources
        BasicResources(p);

        //Bestow stats
        NewBasicStats(p);

        //Bestow behaviours
        BasicBeingBehaviours(p);
        //AttackBehaviour(p);

        //Bestow basic being abilities
        BasicBeingAbilities(p);

        //Bestow abilities
        BasicAttackAbilities(p);
        BasicDefenceAbilities(p);

        //Bestow passive abilities
        //BasicPassiveAbilities(p);

        //bestow defences
         //BasicDefences(p);



        //puts the beings defences in the right order (sorted by defenceSpeed)
        //p.SortDefences();
        //give the Being rule functions like 'check if I'm damaged' etc, these will be called from actionManager during combat
        p.resolutionFunction = new BasicResolutionChecks_FunctionCall(ruleFunctions, p);

        Debug.Log("Being created");

        return p;

       


    }

    public BeingFactory(ActionManager am)
    {
        actionManager = am;
    }


}
