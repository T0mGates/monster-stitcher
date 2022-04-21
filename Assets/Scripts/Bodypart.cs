using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bodypart : MonoBehaviour
{
    public int energyCost;
    public string breed = "Null";
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
    public TextMeshProUGUI uiBreedText;
    public TextMeshProUGUI uiIndexToUseText;
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
    public bool heals;
    public int minHealValue;
    public int maxHealValue;
    public List<int> indexToUse = new List<int>();
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
        uiBreedText = combatManager.uiBreedText;
        uiIndexToUseText = combatManager.uiIndexToUseText;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LevelUp(){
        while(currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            level += 1;
            Debug.Log(gameObject.name + " leveled up to level " + level);
            List<int> buffs = new List<int>();
            int magicBuffIndex = -1;
            int magicPenBuffIndex = -1;
            int attackBuffIndex = -1;
            int attackPenBuffIndex = -1;
            int armorBuffIndex = -1;
            int magicArmorBuffIndex = -1;
            int speedBuffIndex = -1;
            int luckBuffIndex = -1;
            int healthBuffIndex = -1;
            int energyBuffIndex = -1;
            if (magicBuff > 0)
            {
                buffs.Add(magicBuff);
                magicBuffIndex = buffs.Count - 1;
            }
            if (magicPenBuff > 0)
            {
                buffs.Add(magicPenBuff);
                magicPenBuffIndex = buffs.Count - 1;
            }
            if (attackBuff > 0)
            {
                buffs.Add(attackBuff);
                attackBuffIndex = buffs.Count - 1;
            }
            if (attackPenBuff > 0)
            {
                buffs.Add(attackPenBuff);
                attackPenBuffIndex = buffs.Count - 1;
            }
            if (armorBuff > 0)
            {
                buffs.Add(armorBuff);
                armorBuffIndex = buffs.Count - 1;
            }
            if (magicArmorBuff > 0)
            {
                buffs.Add(magicArmorBuff);
                magicArmorBuffIndex = buffs.Count - 1;
            }
            if (speedBuff > 0)
            {
                buffs.Add(speedBuff);
                speedBuffIndex = buffs.Count - 1;
            }
            if (luckBuff > 0)
            {
                buffs.Add(luckBuff);
                luckBuffIndex = buffs.Count - 1;
            }
            if (healthBuff > 0)
            {
                buffs.Add(healthBuff);
                healthBuffIndex = buffs.Count - 1;
            }
            if (energyBuff > 0)
            {
                buffs.Add(energyBuff);
                energyBuffIndex = buffs.Count - 1;
            }
            if (levelToEvolve == level)
            {
                //switch this obj to nextEvolve
            }
            int randNum = Random.Range(0, buffs.Count);
            if(randNum == magicBuffIndex)
            {
                magicBuff += Random.Range(1, 3);
            }
            else if (randNum == magicPenBuffIndex)
            {
                magicPenBuff += Random.Range(1, 3);
            }
            else if (randNum == attackBuffIndex)
            {
                attackBuff += Random.Range(1, 3);
            }
            else if (randNum == attackPenBuffIndex)
            {
                attackPenBuff += Random.Range(1, 3);
            }
            else if (randNum == armorBuffIndex)
            {
                armorBuff += Random.Range(1, 3);
            }
            else if (randNum == magicArmorBuffIndex)
            {
                magicArmorBuff += Random.Range(1, 3);
            }
            else if (randNum == speedBuffIndex)
            {
                speedBuff += Random.Range(1, 3);
            }
            else if (randNum == luckBuffIndex)
            {
                luckBuff += Random.Range(1, 3);
            }
            else if (randNum == healthBuffIndex)
            {
                healthBuff += Random.Range(1, 3);
            }
            else if (randNum == energyBuffIndex)
            {
                energyBuff += Random.Range(1, 3);
            }
            transform.parent.gameObject.GetComponent<Monster>().LevelUpUpdateStats();
            //formula for exp to next level

        }
    }

    public void GainEXP(int amount){
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
            if (heals)
            {
                uiDamageText.text = "Heal Amount: " + minHealValue + " to " + maxHealValue;
            }
            else
            {
                uiDamageText.text = "Damage: " + attackMinValue + " to " + attackMaxValue;
            }
            uiAccuracyText.text = "Accuracy: " + accuracyPercent;
            uiBreedText.text = "Breed: " + breed;
            if (isPhysical)
            {
                uiPhysOrMagicText.text = "Physical";
            }
            else if (isMagical)
            {
                uiPhysOrMagicText.text = "Magical";
            }
            else if (heals)
            {
                uiPhysOrMagicText.text = "Buff";
            }
            uiEnergyCostText.text = "Energy Cost: " + energyCost;
            string indexString = "Positions Required: ";
            if (indexToUse.Contains(0))
            {
                indexString += "Front";
            }
            if (indexToUse.Contains(1))
            {
                if(!indexString.Equals("Positions Required: "))
                {
                    indexString += ", Mid";
                }
                else
                {
                    indexString += "Mid";
                }
            }
            if (indexToUse.Contains(2))
            {
                if (!indexString.Equals("Positions Required: "))
                {
                    indexString += ", Back";
                }
                else
                {
                    indexString += "Back";
                }
            }
            uiIndexToUseText.text = indexString;
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
            if (combatManager.ClickedMonsterAbility(transform.parent.gameObject) && energyCost <= transform.parent.gameObject.GetComponent<Monster>().GetEnergy() && indexToUse.Contains(transform.parent.gameObject.GetComponent<Monster>().currentIndex))
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
                        int damage = DamageFormula(mons, "Physical");
                        Debug.Log(transform.parent.name + " dealt " + damage + " to " + mons + "!");
                        mons.GetComponent<Monster>().TakeDamage(damage);
                    }
                    else if (isMagical)
                    {
                        int damage = DamageFormula(mons, "Magic");
                        Debug.Log(transform.parent.name + " dealt " + damage + " to " + mons + "!");
                        mons.GetComponent<Monster>().TakeDamage(damage);
                    }
                    else if (heals)
                    {
                        int heal = HealFormula(mons);
                        Debug.Log(transform.parent.name + "healed " + heal + " HP from " + mons + "!");
                        mons.GetComponent<Monster>().HealDamage(heal);
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

    public int HealFormula(GameObject mons)
    {
        int damage = Random.Range(minHealValue, maxHealValue + 1);
        int crit = 1;
        float variance = Random.Range(1, 1.13f);
        float stab = 1;
        if (transform.parent.gameObject.GetComponent<Monster>().breeds.Contains(breed))
        {
            stab = 1.5f;
        }
        int randNum = Random.Range(1, 101);
        if (randNum <= transform.parent.gameObject.GetComponent<Monster>().GetLuck() + 2 + extraCritPercent)
        {
            crit += 1;
        }
        damage = (int)(((damage) / 50 + 1.3f) * crit * stab * variance);

        return (damage);
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
            if(mons.gameObject.GetComponent<Monster>().GetArmor() - transform.parent.gameObject.GetComponent<Monster>().GetAttackPen() != 0)
            {
                armorFormula = transform.parent.gameObject.GetComponent<Monster>().GetAttack() / mons.gameObject.GetComponent<Monster>().GetArmor() - transform.parent.gameObject.GetComponent<Monster>().GetAttackPen();
            }
            else
            {
                armorFormula = 1;
            }
        }
        float stab = 1;
        float breedEffects = 1;
        if (transform.parent.gameObject.GetComponent<Monster>().breeds.Contains(breed))
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
        damage = (int)(((damage * armorFormula) / 50 + 1.3f) * crit * breedEffects * stab * variance);

        return(damage);
    }
}
