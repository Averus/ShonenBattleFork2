using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

public enum GameStates { BATTLE };
public GameStates gameState = new GameStates();

    public GameObject actionManagerOb;
    public ActionManager actionManager;
    public BeingFactory beingFactory;
    

    void Start()
    {

        actionManager = actionManagerOb.GetComponent<ActionManager>();
        beingFactory = actionManagerOb.GetComponent<BeingFactory>();




        StartCoroutine(waitToStart());

      
    }

    IEnumerator waitToStart()
    {
        yield return new WaitForSeconds(2);

        //test stuff below

        Being a = beingFactory.CreateBeing("Goku");
        beingFactory.AttackTeamBehaviour(a, 2);

        Being b = beingFactory.CreateBeing("Gohan");
        beingFactory.AttackTeamBehaviour(b, 2);

        Being c = beingFactory.CreateBeing("Trunks");
        beingFactory.AttackTeamBehaviour(c, 2);

        Being d = beingFactory.CreateBeing("Piccolo");
        beingFactory.AttackTeamBehaviour(d, 2);
        beingFactory.DefendTeamReaction(d, 1);

        Being e = beingFactory.CreateBeing("Frieza");
        beingFactory.AttackTeamBehaviour(e, 1);

        Being f = beingFactory.CreateBeing("Cell");
        beingFactory.AttackTeamBehaviour(f, 1);

        Being g = beingFactory.CreateBeing("Buu");
        beingFactory.AttackTeamBehaviour(g, 1);

        Being h = beingFactory.CreateBeing("Saibaman");
        beingFactory.AttackTeamBehaviour(h, 1);
        beingFactory.DefendTeamReaction(h, 2);

        a.team = 1; b.team = 1; c.team = 1; d.team = 1;
        e.team = 2; f.team = 2; g.team = 2; h.team = 2;

        //actionManager.combatants.Add(b);

        //actionManager.combatants.Add(c);



        //gameState = GameStates.BATTLE;
        //actionManager.StartCombat();



    }

}
