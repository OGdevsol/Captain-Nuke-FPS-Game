using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public static PlayerHealthScript instance;
    // Start is called before the first frame update
     public float playerHealth=100;
     private GameUI_HUDManager GUIMgr;
     [HideInInspector] public bool playerDead;
     public int InflictValueDamage;

    private void Awake()
    {
        instance = this;
        GUIMgr = GameUI_HUDManager.instance;
    }

  

    public void DamagePlayer(float InflictValue)
    {
       
        if (playerDead) return;
        {
            InflictValue = InflictValueDamage;
            playerHealth -= InflictValue;
            GameUI_HUDManager.instance.PlayerHealthSlider.value = playerHealth / 100;
            if (playerHealth<=0)
            {
                StartCoroutine(GameUI_HUDManager.instance.LevelFailRoutine());
            }
        }
     //   InflictValue = 0.5f;
       
        Debug.Log("playerHealth" + playerHealth);
       
        
    }
}
