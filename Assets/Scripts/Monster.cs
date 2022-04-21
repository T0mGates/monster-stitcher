using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int expOnDeath;
    private int maxAttack;
    private int currentAttack;
    private int maxAttackPen;
    private int currentAttackPen;
    private int maxMagic;
    private int currentMagic;
    private int maxMagicPen;
    private int currentMagicPen;
    private int maxArmor;
    private int currentArmor;
    private int maxMagicArmor;
    private int currentMagicArmor;
    public int currentSpeed;
    public int maxSpeed;
    private int maxLuck;
    private int currentLuck;
    private int maxHealth;
    private int currentHealth;
    private int currentEnergy;
    private int maxEnergy;
    public Slider hpSlider;
    public Slider energySlider;
    public Slider speedSlider;
    public Transform UIStartPos;
    private CombatManager combatManager;
    public List<string> breeds = new List<string>();
    public int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        breeds.Add("Demon");
        breeds.Add("Holy");
        ResetStats();
        hpSlider.transform.position = UIStartPos.position;
        energySlider.transform.position = UIStartPos.position + new Vector3(0, -0.25f, 0);
        speedSlider.transform.position = UIStartPos.position + new Vector3(0, -0.75f, 0);

        currentHealth -= 3;
        UpdateStatBars();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetEnergy() { return currentEnergy; }

    public void DecreaseSpeed(int amount){
        currentSpeed -= amount;
        UpdateStatBars();
    }

    public void IncreaseEnergy(float percentage)
    {
        currentEnergy += (int)(energySlider.maxValue * percentage);
        if(currentEnergy > energySlider.maxValue)
        {
            currentEnergy = (int)energySlider.maxValue;
        }
        UpdateStatBars();
    }

    public void DecreaseEnergy(int amount)
    {
        currentEnergy -= amount;
        UpdateStatBars();
    }

    public void ResetStats(){
        foreach(Bodypart bodypart in GetComponentsInChildren<Bodypart>()){
            maxMagic += bodypart.magicBuff;
            maxMagicPen += bodypart.magicPenBuff;
            maxAttack += bodypart.attackBuff;
            maxAttackPen += bodypart.attackPenBuff;
            maxArmor += bodypart.armorBuff;
            maxMagicArmor += bodypart.magicArmorBuff;
            maxSpeed += bodypart.speedBuff;
            maxLuck += bodypart.luckBuff;
            maxHealth += bodypart.healthBuff;
            maxEnergy += bodypart.energyBuff;
        }
        currentArmor = maxArmor;
        currentMagic = maxMagic;
        currentMagicPen = maxMagicPen;
        currentAttack = maxAttack;
        currentAttackPen = maxAttackPen;
        currentMagicArmor = maxMagicArmor;
        currentSpeed = maxSpeed;
        currentLuck = maxLuck;
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        UpdateStatBars();
    }

    public void LevelUpUpdateStats()
    {
        int originalMagic = maxMagic;
        int originalMagicPen = maxMagicPen;
        int originalAttack = maxAttack;
        int originalAttackPen = maxAttackPen;
        int originalArmor = maxArmor;
        int originalMagicArmor = maxMagicArmor;
        int originalSpeed = maxSpeed;
        int originalLuck = maxLuck;
        int originalHealth = maxHealth;
        int originalEnergy = maxEnergy;
        maxMagic = 0;
        maxMagicPen = 0;
        maxAttack = 0;
        maxAttackPen = 0;
        maxArmor = 0;
        maxMagicArmor = 0;
        maxSpeed = 0;
        maxLuck = 0;
        maxHealth = 0;
        maxEnergy = 0;
        foreach (Bodypart bodypart in GetComponentsInChildren<Bodypart>())
        {
            maxMagic += bodypart.magicBuff;
            maxMagicPen += bodypart.magicPenBuff;
            maxAttack += bodypart.attackBuff;
            maxAttackPen += bodypart.attackPenBuff;
            maxArmor += bodypart.armorBuff;
            maxMagicArmor += bodypart.magicArmorBuff;
            maxSpeed += bodypart.speedBuff;
            maxLuck += bodypart.luckBuff;
            maxHealth += bodypart.healthBuff;
            maxEnergy += bodypart.energyBuff;
        }
        currentMagic += maxMagic - originalMagic;
        currentMagicPen += maxMagicPen - originalMagicPen;
        currentAttack += maxAttack - originalAttack;
        currentAttackPen += maxAttackPen - originalAttackPen;
        currentArmor += maxArmor - originalArmor;
        currentMagicArmor += maxMagicArmor - originalMagicArmor;
        currentSpeed += maxSpeed - originalSpeed;
        currentLuck += maxLuck - originalLuck;
        currentHealth += maxHealth - originalHealth;
        currentEnergy += maxEnergy - originalEnergy;
        UpdateStatBars();
    }

    public bool AddToSpeedSlider(float amount)
    {
        float multiplier = 1 + (currentSpeed / 100); 
        speedSlider.value += (amount * multiplier);
        return speedSlider.value >= speedSlider.maxValue;
    }

    public void SetSpeedSliderValue(float value)
    {
        speedSlider.value = value;
    }

    public void UpdateStatBars(){
        hpSlider.maxValue = maxHealth;
        hpSlider.value = currentHealth;
        energySlider.maxValue = maxEnergy;
        energySlider.value = currentEnergy;
    }

    public float GetSpeedSliderValue()
    {
        return speedSlider.value;
    }

    public void TakeDamage(int amount)
    {
        //blabla stuff (anims, red glow)
        StartCoroutine(StartTakingDamage(amount));
    }

    public void HealDamage(int amount)
    {
        StartCoroutine(StartHealing(amount));
        //blabla stuff (anims, red glow)
    }

    private IEnumerator StartHealing(int amount)
    {
        float count = 0;
        while(count < amount && currentHealth < maxHealth)
        {
            currentHealth++;
            count++;
            UpdateStatBars();
            yield return new WaitForSeconds(0.1f);
        }
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateStatBars();
    }

    private IEnumerator StartTakingDamage(int amount)
    {
        float count = 0;
        while (count < amount && currentHealth > 0)
        {
            currentHealth--;
            count++;
            UpdateStatBars();
            yield return new WaitForSeconds(0.1f);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateStatBars();
    }

    public void Die()
    {
        combatManager.MonsterDied(gameObject);
        Destroy(gameObject);
    }

    public void GainEXP(int amount)
    {
        foreach (Bodypart bodypart in GetComponentsInChildren<Bodypart>())
        {
            bodypart.GainEXP(amount);
        }
    }

    public int GetLuck() { return currentLuck; }
    public int GetAttack() { return currentAttack; }
    public int GetAttackPen() { return currentAttackPen; }
    public int GetMagic() { return currentMagic; }
    public int GetMagicPen() { return currentMagicPen; }
    public int GetArmor() { return currentArmor; }
    public int GetMagicArmor() { return currentMagicArmor; }
}
