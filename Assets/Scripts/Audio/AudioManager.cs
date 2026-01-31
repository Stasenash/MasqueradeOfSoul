using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header("SFX")]
    [SerializeField] private AudioClip maskAppear;
    [SerializeField] private AudioClip maskHit;
    [SerializeField] private AudioClip footstep;
    [SerializeField] private AudioClip inspectItem;
    [SerializeField] private AudioClip inspectMemory;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip maskCrash;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
    {
        PlayMenuMusic();
        return;
    }

    // если сейчас будет интро Ч музыку Ќ≈ запускаем
    Debug.Log(EndingManager.Instance.IntroPlayed);
    if (!EndingManager.Instance.IntroPlayed)
        return;

    PlayGameplayMusic();
    }

    // ===== MUSIC =====

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameplayMusic()
    {
        Debug.Log("PLAY GAMEPLAY MUSIC");
        PlayMusic(gameplayMusic);
    }

    public void PauseMusic()
    {
        Debug.Log("PAUSE MUSIC");
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        Debug.Log("RESUME MUSIC");
        musicSource.UnPause();
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // ===== SFX =====

    public void PlayMaskAppear()     => sfxSource.PlayOneShot(maskAppear);
    public void PlayMaskHit()        => sfxSource.PlayOneShot(maskHit);
    public void PlayFootstep()       => sfxSource.PlayOneShot(footstep);
    public void PlayInspectItem()    => sfxSource.PlayOneShot(inspectItem);
    public void PlayInspectMemory()  => sfxSource.PlayOneShot(inspectMemory);
    public void PlayDoorOpen()       => sfxSource.PlayOneShot(doorOpen);
    public void PlayMaskCrash()       => sfxSource.PlayOneShot(maskCrash);
}
