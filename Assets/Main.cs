using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

public enum GameStates { BATTLE };
public GameStates gameState = new GameStates();


    public BattleManager battleManager;
    public BeingFactory beingFactory;
    

    void Start()
    {

        battleManager = GetComponent<BattleManager>();
        beingFactory = GetComponent<BeingFactory>();




        StartCoroutine(waitToStart());

      
    }

    IEnumerator waitToStart()
    {
        yield return new WaitForSeconds(2);

        //test stuff below

        Being a = beingFactory.CreateBeing("Tim");
        Being b = beingFactory.CreateBeing("Jeff");
        Being c = beingFactory.CreateBeing("Larry");
        Being d = beingFactory.CreateBeing("Junipur");
        Being e = beingFactory.CreateBeing("Swelt");
        Being f = beingFactory.CreateBeing("Trion");
        Being g = beingFactory.CreateBeing("Moolah");
        Being h = beingFactory.CreateBeing("Dave");

        //battleManager.combatants.Add(b);

        //battleManager.combatants.Add(c);



        //gameState = GameStates.BATTLE;
        //battleManager.StartCombat();



    }

}
