using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas : MonoBehaviour
{
    public static SettingsCanvas Instance { get; private set; }
    public SoundManager soundManager;

    [Header("UI Elements")]
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider overallVolumeSlider;
    public Sprite on;
    public Sprite off;
    public Image music;
    public Image sound;

    public GameObject warning;

    public void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }
    void OnEnable(){
        if (soundManager == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if (obj.name == "SoundManager"){
                    soundManager = obj.GetComponent<SoundManager>();
                }
            }
        }
        // Initialize sliders and toggles with current values from SoundManager
        musicVolumeSlider.value = soundManager.musicSource.volume;
        sfxVolumeSlider.value = soundManager.sfxSource.volume;
        overallVolumeSlider.value = AudioListener.volume;

        // Add listeners to UI elements
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
        overallVolumeSlider.onValueChanged.AddListener(SetOverallVolume);
    }

    public void SetMusicVolume(float volume)
    {
        if (soundManager == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if (obj.name == "SoundManager"){
                    soundManager = obj.GetComponent<SoundManager>();
                }
            }
        }
        soundManager.SetMusicVolume(volume);
    }

    public void SetSfxVolume(float volume)
    {
        if (soundManager == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if (obj.name == "SoundManager"){
                    soundManager = obj.GetComponent<SoundManager>();
                }
            }
        }
        soundManager.SetSfxVolume(volume);
    }

    public void SetOverallVolume(float volume)
    {
        AudioListener.volume = Mathf.Clamp(volume, 0.0f, 1.0f);
    }

    public void MuteMusic()
    {
        soundManager.MuteMusic(true);
        music.sprite = off;
    }

    public void PlayMusic(){
        soundManager.MuteMusic(false);
        music.sprite = on;
    }


    public void MuteSfx()
    {
        soundManager.MuteSfx(true);
        soundManager.PauseEnemySound();
        sound.sprite = off;
    }

    public void PlaySfx(){
        soundManager.MuteSfx(false);
        soundManager.ResumeEnemySound();
        sound.sprite = on;
    }

    public void MuteOverall()
    {
        soundManager.MuteSfx(true);
        soundManager.PauseEnemySound();
        soundManager.MuteMusic(true);
        sound.sprite = off;

        music.sprite = off;
    }

    public void PlaySound(){
        PlaySfx();
        PlayMusic();
        sound.sprite = on;
    }

    public void onMusicClick(){
        if (soundManager == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if (obj.name == "SoundManager"){
                    soundManager = obj.GetComponent<SoundManager>();
                }
            }
        }

        if(music.sprite == on){
            MuteMusic();
        }
        else{
            PlayMusic();
        }
    }

    public void onSoundClick(){
        if (soundManager == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if (obj.name == "SoundManager"){
                    soundManager = obj.GetComponent<SoundManager>();
                }
            }
        }

        if(sound.sprite == on){
            MuteOverall();
        }
        else{
            PlaySound();
        }
    }   

    public void CloseSettingsMenu()
    {
        gameObject.SetActive(false);
    }

    public void resetData(){
        PlayerPrefs.DeleteAll();
        warning.SetActive(false);
    }

    public void showWarning(){
        warning.SetActive(true);
    }

    public void closeWarning(){
        warning.SetActive(false);
    }
}
