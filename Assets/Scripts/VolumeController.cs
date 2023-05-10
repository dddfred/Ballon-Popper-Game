using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class VolumeController : MonoBehaviour
{
    [Header("---------- VolumeController ----------")]
    [SerializeField] AudioMixer MusicMixer;
    [SerializeField] Slider MasterSlider;
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] GameObject pauseCanvas;

    



    private void Awake()
    {
        pauseCanvas = GameObject.FindGameObjectWithTag("pause");

        FindSliders();
    }
    // Start is called before the first frame update
    void Start()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;

        // Load the saved volume value from player prefs
        float savedMainVolume = PlayerPrefs.GetFloat("MainVolume", 1f);
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        MasterSlider.value = savedMainVolume;
        MusicSlider.value = savedMusicVolume;
        SFXSlider.value = savedSFXVolume;

        

        if (PlayerPrefs.HasKey("MainVolume"))
        {
            LoadMainVolume();
        }
        else
        {
            SetMainVolume(savedMainVolume);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume(savedMusicVolume);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            SetSFXVolume(savedSFXVolume);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseCanvas.activeSelf)
        {
            Debug.Log("FIND SLIDERS");
            FindSliders();
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pauseCanvas = GameObject.FindGameObjectWithTag("pause");
        // Find the sliders in the new scene
        FindSliders();

        // Load the volume levels from player prefs
        if (PlayerPrefs.HasKey("MainVolume"))
        {
            LoadMainVolume();
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadMusicVolume();
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadSFXVolume();
        }
    }

    private void FindSliders()
    {

       
        GameObject masterslid = GameObject.FindGameObjectWithTag("MainMusic");
        GameObject musicslid = GameObject.FindGameObjectWithTag("MusicSlider");
        GameObject sfxslid = GameObject.FindGameObjectWithTag("SFX");
        if (masterslid != null)
        {
            MasterSlider = masterslid.GetComponent<Slider>();
            VolumeController vc = FindObjectOfType<VolumeController>();
            if (vc != null)
            {
                MasterSlider.onValueChanged.AddListener(vc.SetMainVolume);
                Debug.Log("FOUND MainSLIDER");
            }
        }
        if (musicslid != null)
        {
            MusicSlider = musicslid.GetComponent<Slider>();
            VolumeController vc = FindObjectOfType<VolumeController>();
            if (vc != null)
            {
                MusicSlider.onValueChanged.AddListener(vc.SetMusicVolume);
                Debug.Log("FOUND MusicSLIDER");
            }
        }
        if (sfxslid != null)
        {
            SFXSlider = sfxslid.GetComponent<Slider>();
            VolumeController vc = FindObjectOfType<VolumeController>();
            if (vc != null)
            {
                SFXSlider.onValueChanged.AddListener(vc.SetSFXVolume);
                Debug.Log("FOUND SFXSLIDER");
            }
        }

        
    }

    public void FindSliddersPause()
    {
        if (!pauseCanvas.activeSelf)
        {
            return;
        }
        else
        {
            Debug.Log("Finding sliders...");
            FindSliders();
        }
    }

    private void OnApplicationQuit()
    {
        float volume = MasterSlider?.value ?? 1f;
        PlayerPrefs.SetFloat("MainVolume", volume);

        volume = MusicSlider?.value ?? 1f;
        PlayerPrefs.SetFloat("MusicVolume", volume);

        volume = SFXSlider?.value ?? 1f;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetMainVolume(float volume)
    {
        MusicMixer.SetFloat("Master", Mathf.Log10(volume) * 20f);

        PlayerPrefs.SetFloat("MainVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        MusicMixer.SetFloat("Music", Mathf.Log10(volume) * 20f);

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        MusicMixer.SetFloat("SFX", Mathf.Log10(volume) * 20f);

        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadMainVolume()
    {
        float loadedVolume = PlayerPrefs.GetFloat("MainVolume");
        MasterSlider.value = loadedVolume;
        SetMainVolume(loadedVolume);
    }

    private void LoadMusicVolume()
    {
        float loadedVolume = PlayerPrefs.GetFloat("MusicVolume");
        MusicSlider.value = loadedVolume;
        SetMusicVolume(loadedVolume);
    }

    private void LoadSFXVolume()
    {
        float loadedVolume = PlayerPrefs.GetFloat("SFXVolume");
        SFXSlider.value = loadedVolume;
        SetSFXVolume(loadedVolume);
    }
}
