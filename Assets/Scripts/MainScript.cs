using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour{
    public int currentLevel = 0;
    public int coins = 0;
    public int enemiesSpawned;
    public bool ableToLoadNextLevel = true;
    
    public GameObject startingPosition;
    public GameObject player;
    public GameObject enemyPrefab;
    public GameObject shopPanel;

    public TMP_Text coinText;

    public int currentDamageLevel = 1;
    public int damageUpgradeCost = 5;
    public TMP_Text damageUpgradeCostText;
    public TMP_Text damageLevelText;
    
    void Start()
    {
        LoadNextLevel();    
    }

    void Update()
    {
        //get current amount of enemies
        enemiesSpawned = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiesSpawned == 0){
            ableToLoadNextLevel = true;
        }else{
            ableToLoadNextLevel = false;
        }
        
        if (Input.GetKeyDown(KeyCode.G)){
            shopPanel.SetActive(!shopPanel.activeSelf);
        }
    }

    public void AddCoins(int amount){
        coins += amount;
        coinText.text = "Coins: " + coins;
    }
    
    public void LoadNextLevel(){
        if (ableToLoadNextLevel){
            currentLevel++; 
            int spawnCount = Random.Range(1, currentLevel * 2);
            for (int i = 0; i < spawnCount; i++){ 
                Instantiate(enemyPrefab, new Vector3(Random.Range(-5, 7), Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
                player.transform.position = startingPosition.transform.position;
            }
        }
    }

    public void BuyDamageUpgrade(){
        if (coins >= damageUpgradeCost){
            currentDamageLevel++;
            coins -= damageUpgradeCost;
            coinText.text = "Coins: " + coins;
            damageUpgradeCost += 5;
            damageUpgradeCostText.text = damageUpgradeCost + "g";
            damageLevelText.text = "Level: " + currentDamageLevel;
        }
    }
}
