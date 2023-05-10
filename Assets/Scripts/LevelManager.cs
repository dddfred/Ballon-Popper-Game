
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Text sceneTxt;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex - 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //restarts the level with a delay
    public void restartLevel()
    {
        //delays the load of scene so animations have time to play
        Invoke("loadCurrentScene", 1f); 
    }

    //restarts the level without a delay
    public void loadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void advanceLevel()
    {
        //delays the load of scene so animations have time to play
        Invoke("nextLevel", 1f);
    }

    //Advances to the next level
    private void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Loads the given scene name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void DisplayCurrentLevel()
    {
        sceneTxt.text = "Scene: " + level;
    }




}
