using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour{
    public int currentLevel = 0;
    public int enemiesSpawned;
    public bool ableToLoadNextLevel = true;
    
    public GameObject enemyPrefab;
    
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
    }
    
    public void LoadNextLevel(){
        if (ableToLoadNextLevel){
            currentLevel++; 
            int spawnCount = Random.Range(1, currentLevel * 2);
            for (int i = 0; i < spawnCount; i++){ 
                Instantiate(enemyPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
            }
        }
        
    }
}
