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
    private int currentMana;
    private int maxMana;
    public Slider hpSlider;
    public Slider manaSlider;
    public Slider speedSlider;
    public Transform UIStartPos;
    // Start is called before the first frame update
    void Start()
    {
        ResetStats();
        hpSlider.transform.position = UIStartPos.position;
        manaSlider.transform.position = UIStartPos.position + new Vector3(0, -0.25f, 0);
        speedSlider.transform.position = UIStartPos.position + new Vector3(0, -0.75f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseSpeed(int amount){
        currentSpeed -= amount;
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
            maxMana += bodypart.manaBuff;
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
        currentMana = maxMana;
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
        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;
    }

    public float GetSpeedSliderValue()
    {
        return speedSlider.value;
    }
}
