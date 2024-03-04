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
    public GameObject snakePrefab;
    public GameObject skeletonPrefab;
    public GameObject zombiePrefab;
    public GameObject shopPanel;

    public TMP_Text coinText;

    public int currentDamageLevel = 1;
    public int damageUpgradeCost = 5;
    public TMP_Text damageUpgradeCostText;
    public TMP_Text damageLevelText;
    
    void Start()
    {
        //random seed
        Random.InitState((int)System.DateTime.Now.Ticks);
        
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
            float spawnCount = Random.Range(1.0f + currentLevel, currentLevel * 1.5f);
            spawnCount = Mathf.Round(spawnCount);
            Debug.Log(spawnCount);
            for (int i = 0; i < spawnCount; i++){
                float chance = Random.Range(0, 100);
                switch (currentLevel){
                    case >= 8:
                        if (chance < 50){
                            Instantiate(skeletonPrefab, new Vector3(Random.Range(-5, 7), Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
                        }else{
                            Instantiate(zombiePrefab, new Vector3(Random.Range(-5, 7), Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
                        }
                        break;
                    case >= 4:
                        if (chance < 50){
                            Instantiate(snakePrefab, new Vector3(Random.Range(-5, 7), Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
                        }else{
                            Instantiate(skeletonPrefab, new Vector3(Random.Range(-5, 7), Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
                        }
                        break;
                    default: //level 1-3
                        Instantiate(snakePrefab, new Vector3(Random.Range(-5, 7), Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
                        break;
                }
                player.transform.position = startingPosition.transform.position;
            }
        }
    }

    public void BuyDamageUpgrade(){
        if (coins >= damageUpgradeCost){
            currentDamageLevel++;
            coins -= damageUpgradeCost;
            coinText.text = "Coins: " + coins;
            damageUpgradeCost += 3;
            damageUpgradeCostText.text = damageUpgradeCost + "g";
            damageLevelText.text = "Level: " + currentDamageLevel;
        }
    }
}
