using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
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

    void Awake(){
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartBattle(null, null, mons1, null, null, mons2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartBattle(GameObject allyMonsterBack, GameObject allyMonsterMid, GameObject allyMonsterFront, GameObject enemyMonsterBack, GameObject enemyMonsterMid, GameObject enemyMonsterFront){
        GameObject monsterToGo = null;
        int lowestSpeed = 1000;
        if(allyMonsterFront != null){
            allyMonsters.Add(allyMonsterFront);
            allyMonsterFront.transform.position = allyMonsterFrontTransform.position;
            if(allyMonsterFront.GetComponent<Monster>().maxSpeed < lowestSpeed){
                monsterToGo = allyMonsterFront;
                lowestSpeed = monsterToGo.GetComponent<Monster>().maxSpeed;
            }
        }
        if(allyMonsterMid != null){
            allyMonsters.Add(allyMonsterMid);
            allyMonsterMid.transform.position = allyMonsterMidTransform.position;
            if(allyMonsterMid.GetComponent<Monster>().maxSpeed < lowestSpeed){
                monsterToGo = allyMonsterMid;
                lowestSpeed = monsterToGo.GetComponent<Monster>().maxSpeed;
            }
        }
        if(allyMonsterBack != null){
            allyMonsters.Add(allyMonsterBack);
            allyMonsterBack.transform.position = allyMonsterBackTransform.position;
            if(allyMonsterBack.GetComponent<Monster>().maxSpeed < lowestSpeed){
                monsterToGo = allyMonsterBack;
                lowestSpeed = monsterToGo.GetComponent<Monster>().maxSpeed;
            }
        }
        if(enemyMonsterFront != null){
            enemyMonsters.Add(enemyMonsterFront);
            enemyMonsterFront.transform.position = enemyMonsterFrontTransform.position;
            if(enemyMonsterFront.GetComponent<Monster>().maxSpeed < lowestSpeed){
                monsterToGo = enemyMonsterFront;
                lowestSpeed = monsterToGo.GetComponent<Monster>().maxSpeed;
            }
        }
        if(enemyMonsterMid != null){
            enemyMonsters.Add(enemyMonsterMid);
            enemyMonsterMid.transform.position = enemyMonsterMidTransform.position;
            if(enemyMonsterMid.GetComponent<Monster>().maxSpeed < lowestSpeed){
                monsterToGo = enemyMonsterMid;
                lowestSpeed = monsterToGo.GetComponent<Monster>().maxSpeed;
            }
        }
        if(enemyMonsterBack != null){
            enemyMonsters.Add(enemyMonsterBack);
            enemyMonsterBack.transform.position = enemyMonsterBackTransform.position;
            if(enemyMonsterBack.GetComponent<Monster>().maxSpeed < lowestSpeed){
                monsterToGo = enemyMonsterBack;
                lowestSpeed = monsterToGo.GetComponent<Monster>().maxSpeed;
            }
        }
        StartTurn(monsterToGo);
    }

    public void StartTurn(GameObject currentMonster){
        foreach(GameObject monster in enemyMonsters){
            monster.GetComponent<Monster>().decreaseSpeed(currentMonster.GetComponent<Monster>().currentSpeed);
        }
        foreach(GameObject monster in allyMonsters){
            monster.GetComponent<Monster>().decreaseSpeed(currentMonster.GetComponent<Monster>().currentSpeed);
        }
        currentMonster.GetComponent<Monster>().resetSpeed();
    }
}
