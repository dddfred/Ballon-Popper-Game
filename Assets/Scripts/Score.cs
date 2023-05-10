using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
    int scoreIncrement = 1;
    public int score;
    public int baseScore = 15; //base score to give when the ball has the original scale
    [SerializeField] Text scoreTxt;
    
    int level;
    Ball_Script bScript;
    LevelManager sceneManager;

    const int score_threshold_per_level = 14;
    int score_threshold;
    // Start is called before the first frame update
    void Start()
    {
        bScript = GameObject.FindObjectOfType<Ball_Script>();
        displayScore();
        sceneManager = GameObject.FindObjectOfType<LevelManager>();
        level = sceneManager.level;
        score_threshold = level * score_threshold_per_level;
        displayLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void addPoints(int score)
    {
        //score += scoreIncrement;
        //Persistent data
        PersistentData.instance.SetScore(score);
        displayScore();
        Debug.Log("SCORE_THRESH: "+score_threshold);
        
        score = PersistentData.instance.GetScore();
        Debug.Log("SCORE:" + score);
        //Advances to the next level if the threshold is met
        if (score >= score_threshold)
        {
            sceneManager.advanceLevel();
        }
        else if(score <= score_threshold)
        {
            sceneManager.restartLevel();
        }
   
    }
    public void displayScore()
    {
        //persistent data
        score = PersistentData.instance.GetScore();
        scoreTxt.text = "Score: " + score;
    }

    //Loads the next Scene
  


    //Calculate Points
    public void calPoints()
    {
        Debug.Log("OUT");
        //calculating the size
        float size = bScript.GetComponent<Transform>().localScale.x / bScript.originalBallSize.x;
        if(size >= bScript.minSize && size <= bScript.maxSize)
        {
            float sizeFactor = 1.0f/size;
            Debug.Log("SIZEFACTOR: "+sizeFactor);
            score = Mathf.RoundToInt(sizeFactor * baseScore);
            Debug.Log("Score:" + score);
        }
        addPoints(score);
    }

    public void displayLevel()
    {
        sceneManager.DisplayCurrentLevel();
    }

    

}
