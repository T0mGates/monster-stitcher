using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum combatState {ENEMY, ALLY, IDLE, WON, LOST}

public class CombatManager : MonoBehaviour
{
    public combatState state;
    public GameObject mons1;
    public GameObject mons2;
    private List<GameObject> enemyMonsters = new List<GameObject>();
    private List<GameObject> allyMonsters = new List<GameObject>();
    public Transform allyMonsterFrontTransform;
    public Transform allyMonsterMidTransform;
    public Transform allyMonsterBackTransform;
    public Transform enemyMonsterFrontTransform;
    public Transform enemyMonsterMidTransform;
    public Transform enemyMonsterBackTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        StartBattle(null, null, mons1, null, null, mons2);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == combatState.IDLE)
        {
            List<GameObject> allyMonstersToGo = new List<GameObject>();
            List<GameObject> enemyMonstersToGo = new List<GameObject>();
            foreach (GameObject monster in allyMonsters)
            {
                bool worked = monster.GetComponent<Monster>().AddToSpeedSlider(0.25f);
                if (worked)
                {
                    allyMonstersToGo.Add(monster);
                }
            }
            foreach(GameObject monster in enemyMonsters)
            {
                bool worked = monster.GetComponent<Monster>().AddToSpeedSlider(0.25f);
                if (worked)
                {
                    enemyMonstersToGo.Add(monster);
                }
            }
            if (allyMonstersToGo.Count > 0)
            {
                foreach(GameObject monster in allyMonstersToGo)
                {
                    if (state == combatState.IDLE)
                    {
                        StartCoroutine(StartAllyTurn(monster));
                    }
                }
            }
            if (enemyMonstersToGo.Count > 0)
            {
                foreach (GameObject monster in enemyMonstersToGo)
                {
                    if(state == combatState.IDLE)
                    {
                        StartCoroutine(StartEnemyTurn(monster));
                    }
                }
            }
        }
    }

    public void StartBattle(GameObject allyMonsterBack, GameObject allyMonsterMid, GameObject allyMonsterFront, GameObject enemyMonsterBack, GameObject enemyMonsterMid, GameObject enemyMonsterFront){
        if(allyMonsterFront != null){
            allyMonsters.Add(allyMonsterFront);
            allyMonsterFront.transform.position = allyMonsterFrontTransform.position;
            allyMonsterFront.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        if(allyMonsterMid != null){
            allyMonsters.Add(allyMonsterMid);
            allyMonsterMid.transform.position = allyMonsterMidTransform.position;
            allyMonsterMid.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        if(allyMonsterBack != null){
            allyMonsters.Add(allyMonsterBack);
            allyMonsterBack.transform.position = allyMonsterBackTransform.position;
            allyMonsterBack.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        if(enemyMonsterFront != null){
            enemyMonsters.Add(enemyMonsterFront);
            enemyMonsterFront.transform.position = enemyMonsterFrontTransform.position;
            enemyMonsterFront.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        if(enemyMonsterMid != null){
            enemyMonsters.Add(enemyMonsterMid);
            enemyMonsterMid.transform.position = enemyMonsterMidTransform.position;
            enemyMonsterMid.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        if(enemyMonsterBack != null){
            enemyMonsters.Add(enemyMonsterBack);
            enemyMonsterBack.transform.position = enemyMonsterBackTransform.position;
            enemyMonsterBack.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        state = combatState.IDLE;
    }

    public IEnumerator StartAllyTurn(GameObject currentMonster){
        state = combatState.ALLY;
        currentMonster.GetComponent<Monster>().SetSpeedSliderValue(0 + (100 - currentMonster.GetComponent<Monster>().GetSpeedSliderValue()));
        yield return new WaitForSeconds(2);
        state = combatState.IDLE;
    }
    public IEnumerator StartEnemyTurn(GameObject currentMonster)
    {
        state = combatState.ENEMY;
        currentMonster.GetComponent<Monster>().SetSpeedSliderValue(0 + (100 - currentMonster.GetComponent<Monster>().GetSpeedSliderValue()));
        yield return new WaitForSeconds(2);
        state = combatState.IDLE;
    }
}
