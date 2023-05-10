using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PersistentData : MonoBehaviour
{

    [SerializeField] int playerScore;
    [SerializeField] string playerName;
    public TMP_InputField nameInput;

    public static PersistentData instance;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerName = "";
        playerScore = 0;
        nameInput.onValueChanged.AddListener(setName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setName(string name)
    {
        playerName = name;
    }

    public void SetScore(int score)
    {
        playerScore += score;
    }

    public string GetName()
    {
        return playerName;
    }

    public int GetScore()
    {
        return playerScore;
    }
}
