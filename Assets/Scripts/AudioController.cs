using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour
{
    [Header("---------- AudioSource ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- AudioClips -----------")]
    public AudioClip backgroundMusic;
    public AudioClip ballDestroyed;

    public static AudioController audioInstance;
    // Start is called before the first frame update

    private void Awake()
    {
        if(audioInstance == null)
        {
            audioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SFXSource= GameObject.FindGameObjectWithTag("ACSFX").GetComponent<AudioSource>();
    }
    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSFX(AudioClip clip)
    {
        if (SFXSource == null)
        {
            Debug.LogError("AudioSource is null");
            return;
        }
        SFXSource.PlayOneShot(clip);
    }

   
}
