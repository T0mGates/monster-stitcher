using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodypart : MonoBehaviour
{   public int magicBuff = 0;
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
    // Start is called before the first frame update
    void Start()
    {       
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
}
