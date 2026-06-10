using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public string sceneName;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource enemySound;

    [Header("Audio Clips")]
    public AudioClip homeScreenMusic;
    public AudioClip gameLevelMusic;
    public AudioClip bossLevelMusic;
    public AudioClip buttonClickSfx;
    public AudioClip playButtonClickSfx;
    public AudioClip enemyDeath;
    public AudioClip enemyHurt;
    public AudioClip levelFail;
    public AudioClip levelComplete;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";
    private const string MusicMutedKey = "MusicMuted";
    private const string SoundMutedKey = "SoundMuted";

    public bool musicMuted;
    public bool soundMuted;

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Play music based on the current scene
        PlayMusicBasedOnScene();
        
        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update sceneName and play music based on the newly loaded scene
        sceneName = scene.name;
        PlayMusicBasedOnScene();
        LoadSettings();
    }

    // Play music
    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayHomeScreenMusic() => PlayMusic(homeScreenMusic);
    public void PlayGameLevelMusic() => PlayMusic(gameLevelMusic);
    public void PlayBossLevelMusic() => PlayMusic(bossLevelMusic);

    // Play a one-shot sound effect
    private void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayButtonClickSfx() => PlaySfx(buttonClickSfx);
    public void PlayPlayButtonClickSfx() => PlaySfx(playButtonClickSfx);
    public void PlayLevelFailSfx() => PlaySfx(levelFail);
    public void PlayLevelCompleteSfx() => PlaySfx(levelComplete);

    // Play enemy sound
    public void PlayEnemySound(AudioClip clip)
    {
        enemySound.clip = clip;
        enemySound.Play();
    }

    public void PlayEnemyDeathSfx() => PlayEnemySound(enemyDeath);
    public void PlayEnemyHurtSfx() => PlayEnemySound(enemyHurt);

    // Stop music or SFX
    public void StopMusic() => musicSource.Stop();
    public void StopSfx() => sfxSource.Stop();
    public void StopEnemySound() => enemySound.Stop();

    // Pause music or SFX
    public void PauseMusic() {
        musicSource.Pause();
        SaveSettings();
    } 
    public void PauseSfx() { 
        sfxSource.Pause(); 
        SaveSettings(); 
    }
    public void PauseEnemySound() => enemySound.Pause();

    // Resume music or SFX
    public void ResumeMusic() {
        musicSource.UnPause();
        SaveSettings();
    }

    public void ResumeSfx(){
        sfxSource.UnPause();
        SaveSettings();
    }
    public void ResumeEnemySound() => enemySound.UnPause();

    // Set volume (0.0 to 1.0) for music and SFX
    public void SetMusicVolume(float volume)
    {
        float clampedVolume = Mathf.Clamp(volume, 0.0f, 1.0f);
        musicSource.volume = clampedVolume;
        SaveSettings();
    }

    public void SetSfxVolume(float volume)
    {
        float clampedVolume = Mathf.Clamp(volume, 0.0f, 1.0f);
        sfxSource.volume = clampedVolume;
        enemySound.volume = clampedVolume; // Apply the same volume to the enemy sound effects
        SaveSettings();
    }

    // Mute/Unmute Music
    public void MuteMusic(bool mute)
    {
        musicSource.mute = mute;
        musicMuted = mute;
        SaveSettings();
    }

    public void MuteSfx(bool mute)
    {
        sfxSource.mute = mute;
        enemySound.mute = mute;
        soundMuted = mute;
        SaveSettings();
    }

    // Save settings to PlayerPrefs
    private void SaveSettings()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, musicSource.volume);
        PlayerPrefs.SetFloat(SfxVolumeKey, sfxSource.volume);
        PlayerPrefs.SetInt(MusicMutedKey, musicMuted ? 1 : 0);
        PlayerPrefs.SetInt(SoundMutedKey, soundMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Load settings from PlayerPrefs
    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
            musicSource.volume = musicVolume;
        }
        if (PlayerPrefs.HasKey(SfxVolumeKey))
        {
            float sfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey);
            sfxSource.volume = sfxVolume;
            enemySound.volume = sfxVolume;
        }
        if (PlayerPrefs.HasKey(MusicMutedKey))
        {
            musicMuted = PlayerPrefs.GetInt(MusicMutedKey) == 1;
            musicSource.mute = musicMuted;
        }
        if (PlayerPrefs.HasKey(SoundMutedKey))
        {
            soundMuted = PlayerPrefs.GetInt(SoundMutedKey) == 1;
            sfxSource.mute = soundMuted;
            enemySound.mute = soundMuted;
        }
    }

    // Handle music based on the scene
    private void PlayMusicBasedOnScene()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != "MainScene" && musicSource.clip != gameLevelMusic && sceneName != "Level10" && musicMuted == false)
        {
            PlayGameLevelMusic();
        }
        else if (sceneName == "MainScene" && musicSource.clip != homeScreenMusic && musicMuted == false)
        {
            PlayHomeScreenMusic();
        }
        else if (sceneName == "Level10" && musicSource.clip != bossLevelMusic && musicMuted == false)
        {
            PlayBossLevelMusic();
        }
    }
}
