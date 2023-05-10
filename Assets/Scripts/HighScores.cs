using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HighScores : MonoBehaviour
{
    public const int NUM_HIGH_SCORES = 5;
    public const string NAME_KEY = "HSName";
    public const string SCORE_KEY = "HScore";
    [SerializeField] string playerName;
    [SerializeField] int playerScore;

    [SerializeField] Text[] nameTexts;
    [SerializeField] Text[] scoreTexts;

    // Start is called before the first frame update
    void Start()
    {
         if (nameTexts.Length < NUM_HIGH_SCORES)
        {
            Array.Resize(ref nameTexts, NUM_HIGH_SCORES);
        }

        if (scoreTexts.Length < NUM_HIGH_SCORES)
        {
            Array.Resize(ref scoreTexts, NUM_HIGH_SCORES);
        }


        playerName = PersistentData.instance.GetName();
        playerScore = PersistentData.instance.GetScore();

        SaveScore();
        DisplayHighScores();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveScore()
    {
        for (int i = 1; i <= NUM_HIGH_SCORES; i++)
        {
            string currentNameKey = NAME_KEY + i;
            string currentScoreKey = SCORE_KEY + i;

            if (PlayerPrefs.HasKey(currentScoreKey))
            {
                int currentScore = PlayerPrefs.GetInt(currentScoreKey);
                if (playerScore > currentScore)
                {
                    int tempScore = currentScore;
                    string tempName = PlayerPrefs.GetString(currentNameKey);

                    Debug.Log("Swapping " + playerName + " (" + playerScore + ") with " + tempName + " (" + tempScore + ")");

                    PlayerPrefs.SetString(currentNameKey, playerName);
                    PlayerPrefs.SetInt(currentScoreKey, playerScore);

                    playerName = tempName;
                    playerScore = tempScore;
                }
            }
            else
            {
                Debug.Log("Saving " + playerName + " (" + playerScore + ")");

                PlayerPrefs.SetString(currentNameKey, playerName);
                PlayerPrefs.SetInt(currentScoreKey, playerScore);
                return;
            }
        }
    }

    public void DisplayHighScores()
    {

        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            nameTexts[i].text = PlayerPrefs.GetString(NAME_KEY + (i + 1));
            scoreTexts[i].text = PlayerPrefs.GetInt(SCORE_KEY + (i + 1)).ToString();
            Debug.Log("Name " + (i + 1) + ": " + nameTexts[i].text);
        }
    }
}