using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum combatState {ENEMY, ALLY, IDLE, WON, LOST}

public class CombatManager : MonoBehaviour
{
    public combatState state;
    public GameObject mons1;
    public GameObject mons2;
    public GameObject mons3;
    public GameObject mons4;
    private List<GameObject> enemyMonsters = new List<GameObject>();
    private List<GameObject> allyMonsters = new List<GameObject>();
    public Transform allyMonsterFrontTransform;
    public Transform allyMonsterMidTransform;
    public Transform allyMonsterBackTransform;
    public Transform enemyMonsterFrontTransform;
    public Transform enemyMonsterMidTransform;
    public Transform enemyMonsterBackTransform;
    private GameObject currentMonsterTurn;
    private bool waitingOnClick = false;
    public GameObject cancelButton;
    private GameObject[] gettingHit = new GameObject[3];
    public GameObject uiObject;
    public TextMeshProUGUI uiInfoText;
    public TextMeshProUGUI uiDamageText;
    public TextMeshProUGUI uiAccuracyText;
    public TextMeshProUGUI uiEnergyCostText;
    public TextMeshProUGUI uiPhysOrMagicText;
    public TextMeshProUGUI uiTypeText;
    public bool targetAllyHolder;
    public bool targetEnemyHolder;
    public int numTargetsHolder;
    public GameObject bodypartHolder;
    public int maxIndexHolder;
    public bool clickAgainHolder;

    // Start is called before the first frame update
    void Start()
    {
        StartBattle(null, null, mons1, mons2, mons3, mons4);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == combatState.IDLE)
        {
            IncrementSpeed();
        }
    }

    private void IncrementSpeed()
    {
        List<GameObject> allyMonstersToGo = new List<GameObject>();
        List<GameObject> enemyMonstersToGo = new List<GameObject>();
        foreach (GameObject monster in allyMonsters)
        {
            bool worked = monster.GetComponent<Monster>().AddToSpeedSlider(50 * Time.deltaTime);
            if (worked)
            {
                allyMonstersToGo.Add(monster);
            }
        }
        foreach (GameObject monster in enemyMonsters)
        {
            bool worked = monster.GetComponent<Monster>().AddToSpeedSlider(50 * Time.deltaTime);
            if (worked)
            {
                enemyMonstersToGo.Add(monster);
            }
        }
        if (allyMonstersToGo.Count > 0)
        {
            foreach (GameObject monster in allyMonstersToGo)
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
                if (state == combatState.IDLE)
                {
                    StartCoroutine(StartEnemyTurn(monster));
                }
            }
        }
    }

    public void StartBattle(GameObject allyMonsterBack, GameObject allyMonsterMid, GameObject allyMonsterFront, GameObject enemyMonsterFront, GameObject enemyMonsterMid, GameObject enemyMonsterBack){
        if(allyMonsterFront != null){
            GameObject newMonster = Instantiate(allyMonsterFront, allyMonsterFrontTransform);
            allyMonsters.Add(newMonster);
            allyMonsterFront.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        if(allyMonsterMid != null){
            GameObject newMonster = Instantiate(allyMonsterMid, allyMonsterMidTransform);
            allyMonsters.Add(newMonster);
            allyMonsterMid.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        if(allyMonsterBack != null){
            GameObject newMonster = Instantiate(allyMonsterBack, allyMonsterBackTransform);
            allyMonsters.Add(newMonster);
            allyMonsterBack.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        if(enemyMonsterFront != null){
            GameObject newMonster = Instantiate(enemyMonsterFront, enemyMonsterFrontTransform);
            enemyMonsters.Add(newMonster);
            newMonster.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        if(enemyMonsterMid != null){
            GameObject newMonster = Instantiate(enemyMonsterMid, enemyMonsterMidTransform);
            enemyMonsters.Add(newMonster);
            enemyMonsterMid.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        if(enemyMonsterBack != null){
            GameObject newMonster = Instantiate(enemyMonsterBack, enemyMonsterBackTransform);
            enemyMonsters.Add(newMonster);
            enemyMonsterBack.GetComponent<Monster>().SetSpeedSliderValue(0);
        }
        state = combatState.IDLE;
    }

    public IEnumerator StartAllyTurn(GameObject currentMonster){
        state = combatState.ALLY;
        currentMonster.GetComponent<Monster>().IncreaseEnergy(0.2f);
        currentMonsterTurn = currentMonster;
        currentMonster.GetComponent<Monster>().SetSpeedSliderValue(0 + (100 - currentMonster.GetComponent<Monster>().GetSpeedSliderValue()));
        while (currentMonsterTurn != null)
        {
            yield return new WaitForSeconds(0.5f);

        }
        state = combatState.IDLE;
    }
    public IEnumerator StartEnemyTurn(GameObject currentMonster)
    {
        state = combatState.ENEMY;
        currentMonster.GetComponent<Monster>().IncreaseEnergy(0.2f);
        currentMonsterTurn = currentMonster;
        currentMonster.GetComponent<Monster>().SetSpeedSliderValue(0 + (100 - currentMonster.GetComponent<Monster>().GetSpeedSliderValue()));
        //while (currentMonsterTurn != null)
        //{
            yield return new WaitForSeconds(0.5f);

        //}
        state = combatState.IDLE;
    }

    public bool ClickedMonsterAbility(GameObject monster)
    {
        return ((monster == currentMonsterTurn) && !waitingOnClick && allyMonsters.Contains(monster));
    }

    public void WaitForEnemyClick()
    {
        cancelButton.SetActive(true);
        waitingOnClick = true;
    }

    public bool IsWaitingOnClick()
    {
        return waitingOnClick;
    }

    public void CancelButtonClicked()
    {
        waitingOnClick = false;
        currentMonsterTurn.GetComponentInChildren<Bodypart>().uiObject.SetActive(false);
        for (int i = 0; i < gettingHit.Length; i++)
        {
            gettingHit[i] = null;
        }
        cancelButton.SetActive(false);
    }

    public void ClickedMonster(GameObject clickedMonster)
    {
        if (enemyMonsters.Contains(clickedMonster) && targetEnemyHolder)
        {
            Debug.Log("IN is monster and can target enemy");
            if(enemyMonsters.IndexOf(clickedMonster) <= maxIndexHolder)
            {
                Debug.Log("IN is a monster I can hit (depending on back/front)");
                if (numTargetsHolder == 1)
                {
                    Debug.Log("IN only 1 target, attacks!");
                    gettingHit[0] = clickedMonster;
                    currentMonsterTurn = null;
                    bodypartHolder.GetComponent<Bodypart>().UseAttack(gettingHit);
                    waitingOnClick = false;
                    cancelButton.SetActive(false);
                    for (int i = 0; i < gettingHit.Length; i++)
                    {
                        gettingHit[i] = null;
                    }
                }
                else if(numTargetsHolder > 1 && !clickAgainHolder)
                {
                    Debug.Log("IN i have more than one target but i dont choose which, attacks!");
                    for (int i = 0; i < numTargetsHolder; i++)
                    {
                        gettingHit[i] = enemyMonsters[i];
                    }
                    currentMonsterTurn = null;
                    bodypartHolder.GetComponent<Bodypart>().UseAttack(gettingHit);
                    waitingOnClick = false;
                    cancelButton.SetActive(false);
                    for (int i = 0; i < gettingHit.Length; i++)
                    {
                        gettingHit[i] = null;
                    }
                }
                else if(numTargetsHolder > 1 && clickAgainHolder)
                {
                    Debug.Log("IN i have more than one target but I can hit again");
                    int count = 0;
                    bool containsMonster = false;
                    foreach(GameObject mons in gettingHit)
                    {
                        if(mons != null)
                        {
                            count++;
                        }
                        if(mons == clickedMonster)
                        {
                            containsMonster = true;
                        }
                    }
                    if(gettingHit[count] == null && !containsMonster)
                    {
                        gettingHit[count] = clickedMonster;
                        count++;
                    }
                    Debug.Log(count);
                    if(count == enemyMonsters.Count)
                    {
                        clickAgainHolder = false;
                    }
                    if(numTargetsHolder == count)
                    {
                        clickAgainHolder = false;
                    }
                    if (!clickAgainHolder)
                    {
                        Debug.Log("IN attack the different enemies that I chose");
                        currentMonsterTurn = null;
                        bodypartHolder.GetComponent<Bodypart>().UseAttack(gettingHit);
                        waitingOnClick = false;
                        cancelButton.SetActive(false);
                        for (int i = 0; i < gettingHit.Length; i++)
                        {
                            gettingHit[i] = null;
                        }
                    }
                }
            }
        }
        else if (allyMonsters.Contains(clickedMonster) && targetAllyHolder)
        {
            Debug.Log("IN is monster and can target ally");
            if (allyMonsters.IndexOf(clickedMonster) <= maxIndexHolder)
            {
                Debug.Log("IN is a monster I can hit (depending on back/front)");
                if (numTargetsHolder == 1)
                {
                    Debug.Log("IN only 1 target, acts!");
                    gettingHit[0] = clickedMonster;
                    currentMonsterTurn = null;
                    bodypartHolder.GetComponent<Bodypart>().UseAttack(gettingHit);
                    waitingOnClick = false;
                    cancelButton.SetActive(false);
                    for (int i = 0; i < gettingHit.Length; i++)
                    {
                        gettingHit[i] = null;
                    }
                }
                else if (numTargetsHolder > 1 && !clickAgainHolder)
                {
                    Debug.Log("IN i have more than one target but i dont choose which, acts!");
                    for (int i = 0; i < numTargetsHolder; i++)
                    {
                        gettingHit[i] = allyMonsters[i];
                    }
                    currentMonsterTurn = null;
                    bodypartHolder.GetComponent<Bodypart>().UseAttack(gettingHit);
                    waitingOnClick = false;
                    cancelButton.SetActive(false);
                    for (int i = 0; i < gettingHit.Length; i++)
                    {
                        gettingHit[i] = null;
                    }
                }
                else if (numTargetsHolder > 1 && clickAgainHolder)
                {
                    Debug.Log("IN i have more than one target but I can hit again");
                    int count = 0;
                    bool containsMonster = false;
                    foreach (GameObject mons in gettingHit)
                    {
                        if (mons != null)
                        {
                            count++;
                        }
                        if (mons == clickedMonster)
                        {
                            containsMonster = true;
                        }
                    }
                    if (gettingHit[count] == null && !containsMonster)
                    {
                        gettingHit[count] = clickedMonster;
                        count++;
                    }
                    Debug.Log(count);
                    if (count == allyMonsters.Count)
                    {
                        clickAgainHolder = false;
                    }
                    if (numTargetsHolder == count)
                    {
                        clickAgainHolder = false;
                    }
                    if (!clickAgainHolder)
                    {
                        Debug.Log("IN attack the different allies that I chose");
                        currentMonsterTurn = null;
                        bodypartHolder.GetComponent<Bodypart>().UseAttack(gettingHit);
                        waitingOnClick = false;
                        cancelButton.SetActive(false);
                        for (int i = 0; i < gettingHit.Length; i++)
                        {
                            gettingHit[i] = null;
                        }
                    }
                }
            }
        }
    }
}
