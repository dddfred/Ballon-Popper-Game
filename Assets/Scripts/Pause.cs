using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public GameObject pauseMenu;
    public static bool isPaused;
    private LevelManager sceneManager;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Slider masterSlider; // reference to the Master slider in the Pause canvas
    [SerializeField] private Slider musicSlider; // reference to the Music slider in the Pause canvas
    [SerializeField] private Slider sfxSlider; // reference to the SFX slider in the Pause canvas

    [SerializeField] private VolumeController volumeController;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        sceneManager = GameObject.FindObjectOfType<LevelManager>();
        
        if (volumeController != null)
        {
            if (masterSlider != null)
            {
                masterSlider.onValueChanged.AddListener(volumeController.SetMainVolume);
            }

            if (musicSlider != null)
            {
                musicSlider.onValueChanged.AddListener(volumeController.SetMusicVolume);
            }

            if (sfxSlider != null)
            {
                sfxSlider.onValueChanged.AddListener(volumeController.SetSFXVolume);
            }
        }
        AssignVolumeController();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
                
        }
        
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        volumeController = GameObject.FindObjectOfType<VolumeController>();
        FindObjectOfType<VolumeController>().FindSliddersPause();
        AssignVolumeController();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        sceneManager.loadCurrentScene();
        isPaused = false;
    }

    public void BackToMainMenu(string sceneName)
    {
        Time.timeScale = 1f;
        sceneManager.LoadScene(sceneName);
        isPaused = false;
    }

    private void AssignVolumeController()
    {
        // Find the sliders on the pause canvas
        Slider masterSlider = pauseMenuUI.transform.Find("MainSlider").GetComponent<Slider>();
        Slider musicSlider = pauseMenuUI.transform.Find("MusicSlider").GetComponent<Slider>();
        Slider sfxSlider = pauseMenuUI.transform.Find("SFXSlider").GetComponent<Slider>();

        // Assign their onValueChanged parameter to their corresponding methods in the VolumeController script
        if (volumeController != null)
        {
            masterSlider.onValueChanged.AddListener(volumeController.SetMainVolume);
            musicSlider.onValueChanged.AddListener(volumeController.SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(volumeController.SetSFXVolume);
        }
    }


}
