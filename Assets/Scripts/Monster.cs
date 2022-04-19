using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        updateStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseSpeed(int amount){
        currentSpeed -= amount;
    }
    public void resetSpeed(){
        currentSpeed = maxSpeed;
    }

    public void updateStats(){
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
    }
}
