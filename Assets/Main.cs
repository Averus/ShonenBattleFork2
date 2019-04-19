using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

public enum GameStates { BATTLE };
public GameStates gameState = new GameStates();


    public ActionManager actionManager;
    public BeingFactory beingFactory;
    

    void Start()
    {

        actionManager = GetComponent<ActionManager>();
        beingFactory = GetComponent<BeingFactory>();




        StartCoroutine(waitToStart());

      
    }

    IEnumerator waitToStart()
    {
        yield return new WaitForSeconds(2);

        //test stuff below

        Being a = beingFactory.CreateBeing("Goku");
        Being b = beingFactory.CreateBeing("Gohan");
        Being c = beingFactory.CreateBeing("Trunks");
        Being d = beingFactory.CreateBeing("Piccolo");
        Being e = beingFactory.CreateBeing("Frieza");
        Being f = beingFactory.CreateBeing("Cell");
        Being g = beingFactory.CreateBeing("Buu");
        Being h = beingFactory.CreateBeing("Saibaman");

        a.team = 1; b.team = 1; c.team = 1; d.team = 1;
        e.team = 2; f.team = 2; g.team = 2; h.team = 2;

        //actionManager.combatants.Add(b);

        //actionManager.combatants.Add(c);



        //gameState = GameStates.BATTLE;
        //actionManager.StartCombat();



    }

}
