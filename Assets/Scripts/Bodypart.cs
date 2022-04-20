using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bodypart : MonoBehaviour
{
    public int energyCost;
    public string type = "Null";
    public int numTargets = 1;
    public string nameOfPart;
    public int attackMinValue;
    public int attackMaxValue;
    public int accuracyPercent;
    public int extraCritPercent;
    public string infoText;
    public GameObject uiObject;
    public TextMeshProUGUI uiInfoText;
    public TextMeshProUGUI uiDamageText;
    public TextMeshProUGUI uiAccuracyText;
    public TextMeshProUGUI uiEnergyCostText;
    public TextMeshProUGUI uiPhysOrMagicText;
    public TextMeshProUGUI uiTypeText;
    public int energyBuff = 0;
    public int magicBuff = 0;
    public int magicPenBuff = 0;
    public int attackBuff = 0;
    public int attackPenBuff = 0;
    public int armorBuff = 0;
    public int magicArmorBuff = 0;
    public int speedBuff = 0;
    public int luckBuff = 0;
    public int healthBuff = 0;
    public int currentExp = 0;
    public int expToNextLevel = 5;
    public int level = 1;
    public int levelToEvolve;
    public GameObject nextEvolve;
    private CombatManager combatManager;
    public bool targetEnemy;
    public bool targetAlly;
    public int maxIndexToHit = 0;
    public bool clickMoreThanOnce = false;
    public bool isPhysical;
    public bool isMagical;
    // Start is called before the first frame update
    void Start()
    {
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        uiObject = combatManager.uiObject;
        uiInfoText = combatManager.uiInfoText;
        uiDamageText = combatManager.uiDamageText;
        uiAccuracyText = combatManager.uiAccuracyText;
        uiEnergyCostText = combatManager.uiEnergyCostText;
        uiPhysOrMagicText = combatManager.uiPhysOrMagicText;
        uiTypeText = combatManager.uiTypeText;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LevelUp(){
        level += 1;
        if(levelToEvolve == level){
            //switch this obj to nextEvolve
        }
        //formula for exp to next level
        currentExp = 0;
    }

    public void GainExp(int amount){
        currentExp += amount;
        if(currentExp >= expToNextLevel){
            LevelUp();
        }
    }

    public void useAttack(GameObject enemy){
        //use attack or whatever
    }

    void OnMouseEnter(){
        if (!combatManager.IsWaitingOnClick())
        {
            uiObject.SetActive(true);
            uiInfoText.text = nameOfPart + ": " + infoText;
            uiDamageText.text = "Damage: " + attackMinValue + " to " + attackMaxValue;
            uiAccuracyText.text = "Accuracy: " + accuracyPercent;
            uiTypeText.text = "Type: " + type;
            if (isPhysical)
            {
                uiPhysOrMagicText.text = "Physical";
            }
            else if (isMagical)
            {
                uiPhysOrMagicText.text = "Magical";
            }
            uiEnergyCostText.text = "Energy Cost: " + energyCost;
            uiObject.transform.position = transform.parent.gameObject.transform.position + new Vector3(0, 2, 0);
        }
    }

    void OnMouseExit(){
        if (!combatManager.IsWaitingOnClick())
        {
            uiObject.SetActive(false);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatManager.ClickedMonsterAbility(transform.parent.gameObject) && energyCost <= transform.parent.gameObject.GetComponent<Monster>().GetEnergy())
            {
                combatManager.WaitForEnemyClick();
                combatManager.targetAllyHolder = targetAlly;
                combatManager.targetEnemyHolder = targetEnemy;
                combatManager.numTargetsHolder = numTargets;
                combatManager.bodypartHolder = gameObject;
                combatManager.maxIndexHolder = maxIndexToHit;
                combatManager.clickAgainHolder = clickMoreThanOnce;
            }
            else if (combatManager.IsWaitingOnClick())
            {
                combatManager.ClickedMonster(transform.parent.gameObject);
            }
        }
    }

    public void UseAttack(GameObject[] monstersHit)
    {
        //check for nulls
        //animation

        uiObject.SetActive(false);
        transform.parent.gameObject.GetComponent<Monster>().DecreaseEnergy(energyCost);
        foreach (GameObject mons in monstersHit)
        {
            if(mons != null)
            {
                int randNum = Random.Range(1, 101);
                if(randNum <= accuracyPercent)
                {
                    if (isPhysical)
                    {
                        mons.GetComponent<Monster>().TakeDamage(DamageFormula(mons, "Physical"));
                    }
                    else if (isMagical)
                    {
                        mons.GetComponent<Monster>().TakeDamage(DamageFormula(mons, "Magic"));
                    }
                }
                else
                {
                    //miss!
                }
            }
        }
        //make them take dmg (probs just do it thru monster script)
    }

    public int DamageFormula(GameObject mons, string attackType)
    {
        int damage = Random.Range(attackMinValue, attackMaxValue + 1);
        int crit = 1;
        float variance = Random.Range(1, 1.13f);
        float armorFormula = 0;
        if(attackType == "Magic")
        { 
            if(mons.gameObject.GetComponent<Monster>().GetMagicArmor() - transform.parent.gameObject.GetComponent<Monster>().GetMagicPen() != 0)
            {
                armorFormula = transform.parent.gameObject.GetComponent<Monster>().GetMagic() / mons.gameObject.GetComponent<Monster>().GetMagicArmor() - transform.parent.gameObject.GetComponent<Monster>().GetMagicPen();
            }
            else
            {
                armorFormula = 1;
            }
        }
        else if(attackType == "Physical")
        {
            if(transform.parent.gameObject.GetComponent<Monster>().GetAttack() / mons.gameObject.GetComponent<Monster>().GetArmor() - transform.parent.gameObject.GetComponent<Monster>().GetAttackPen() != 0)
            {
                armorFormula = transform.parent.gameObject.GetComponent<Monster>().GetAttack() / mons.gameObject.GetComponent<Monster>().GetArmor() - transform.parent.gameObject.GetComponent<Monster>().GetAttackPen();
            }
            else
            {
                armorFormula = 1;
            }
        }
        float stab = 1;
        float typeEffects = 1;
        if (transform.parent.gameObject.GetComponent<Monster>().types.Contains(type))
        {
            stab = 1.5f;
        }
        if (armorFormula < 1)
        {
            armorFormula = 1;
        }
        int randNum = Random.Range(1, 101);
        if (randNum <= transform.parent.gameObject.GetComponent<Monster>().GetLuck() + 2 + extraCritPercent)
        {
            crit += 1;
        }
        damage = (int)(((damage * armorFormula) / 50 + 1.3f) * crit * typeEffects * stab * variance);
        Debug.Log("Damage: " + damage);
        Debug.Log("crit: " + crit);
        Debug.Log("variance: " + variance);
        Debug.Log("armor formula: " + armorFormula);
        Debug.Log("type effects: " + typeEffects);
        Debug.Log("stab: " + stab);
        return(damage);
    }
}
