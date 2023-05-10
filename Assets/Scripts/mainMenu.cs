using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    
    LevelManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startButton(string StartScene)
    {
        sceneManager.LoadScene(StartScene);
    }

    public void gameInstructions(string Instructions)
    {
        sceneManager.LoadScene(Instructions);
    }

    public void gameSettings(string gameSettings)
    {
        sceneManager.LoadScene(gameSettings);
    }

}
