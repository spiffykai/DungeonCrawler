using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreenScript : MonoBehaviour{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RestartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    
    public void QuitGame(){
        Application.Quit();
    }
}
