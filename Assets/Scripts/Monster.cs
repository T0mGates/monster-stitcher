using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
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
    public List<string> types = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        types.Add("Demon");
        ResetStats();
        hpSlider.transform.position = UIStartPos.position;
        energySlider.transform.position = UIStartPos.position + new Vector3(0, -0.25f, 0);
        speedSlider.transform.position = UIStartPos.position + new Vector3(0, -0.75f, 0);
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
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateStatBars();
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
