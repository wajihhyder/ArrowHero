using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject pauseCanvas;         // Pause menu
    public GameObject gameoverCanvas;      // Game over screen
    public GameObject levelCompletedCanvas; // Level completed screen
    public GameObject levelGame;              // Main game screen
    public GameObject prologueCanvas;
    public SoundManager soundManager;
    public GameObject settingsCanvas;
    public GameObject playerSpawn;
    public GameObject EnemyPrefab;
    public GameObject PlayerPrefab;

    public ScoreManager scoreManager;
    public GameObject player;
    public Weapon weapon;

    public int health;

    PersistentData persistentData = PersistentData.Instance;
    int level;

    bool check;

    public Enemies enemies;

    int arrows;

    public LevelCompleted levelCompleted;
    private const string PrologueShownKey = "PrologueShown";

    void Awake(){
        
        GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
        foreach (GameObject obj in temp){
            if(obj.name == "PauseCanvas"){
                pauseCanvas = obj;
            }
            else if(obj.name == "GameoverCanvas"){
                gameoverCanvas = obj;
            }
            else if(obj.name == "LevelCompletedCanvas"){
                levelCompletedCanvas = obj;
                levelCompleted = obj.GetComponent<LevelCompleted>();
            }
            else if(obj.name == "Game"){
                levelGame = obj;
            }
            else if (obj.name == "Game 1"){
                levelGame = obj;
            }
            else if(obj.name == "PlayerSpawn"){
                playerSpawn = obj;
            }
            else if(obj.tag == "Player"){
                player = obj;
                weapon = obj.GetComponent<Weapon>();
                arrows = weapon.arrowCount;
            }
            else if(obj.name == "ScoreManager"){
                scoreManager = obj.GetComponent<ScoreManager>();
            }
            else if(obj.name == "Enemies"){
                enemies = obj.GetComponent<Enemies>();
            }
            else if(obj.name == "SoundManager"){
                soundManager = obj.GetComponent<SoundManager>();
            }
            else if(obj.name == "SettingsCanvas"){
                settingsCanvas = obj;
            }
        }
    
        level = persistentData.currentLevel;

        pauseCanvas.SetActive(false);
        gameoverCanvas.SetActive(false);
        levelCompletedCanvas.SetActive(false);
        levelGame.SetActive(true);

        if(!PlayerPrefs.HasKey(PrologueShownKey) && level == 1){
            showPrologue();
        }
        else{
            ShowGameCanvas();
        }
    }

    // Show the home screen
    public void ShowHomeCanvas()
    {
        SetCanvasVisibility(pauseCanvas, false);
        SetCanvasVisibility(gameoverCanvas, false);
        SetCanvasVisibility(levelCompletedCanvas, false);
        SetCanvasVisibility(levelGame, false);
        SetCanvasVisibility(prologueCanvas, false);
        Time.timeScale = 1f;
    }

    // Show the level selection screen
    public void ShowLevelCanvas()
    {
        SetCanvasVisibility(pauseCanvas, false);
        SetCanvasVisibility(gameoverCanvas, false);
        SetCanvasVisibility(levelCompletedCanvas, false);
        SetCanvasVisibility(levelGame, false);
    }

    // Show the pause menu
    public void ShowPauseCanvas()
    {
        SetCanvasVisibility(pauseCanvas, true);
    }

    private IEnumerator WaitOneSecond()
    {
        // Wait for one second
        yield return new WaitForSeconds(1.0f);
    }

    // Show the game over screen
    public void ShowGameoverCanvas()
    {
        StartCoroutine(WaitOneSecond());

        SetCanvasVisibility(pauseCanvas, false);
        SetCanvasVisibility(gameoverCanvas, true);
        SetCanvasVisibility(levelCompletedCanvas, false);
        SetCanvasVisibility(levelGame, false);

        soundManager.PauseMusic();
        soundManager.PlayLevelFailSfx();
    }

    // Show the level completed screen
    public void ShowLevelCompletedCanvas()
    {
        StartCoroutine(WaitOneSecond());

        SetCanvasVisibility(pauseCanvas, false);
        SetCanvasVisibility(gameoverCanvas, false);
        SetCanvasVisibility(levelCompletedCanvas, true);
        SetCanvasVisibility(levelGame, false);
        
        level = persistentData.currentLevel;

        if(level < 9){
            Debug.Log(level);
            check = persistentData.checkUnlocked(level);
            if(check == false){
                persistentData.unlockLevel(level);
            }
        }
    
    
        int enemiesCount = enemies.enemiesCount;
        int crowns = 0;
        int arrowBonus = 0;
        int noHitBonus = 0;
        int headshotBonus = 0;
        int coins = 0;

        if(enemiesCount == 1){
            if(scoreManager.score >= 5 && weapon.arrowCount == arrows - 1){
                crowns = 3; 
                arrowBonus = 10;
            }
            else if(scoreManager.score >= 5 && weapon.arrowCount >= arrows - 3){
                crowns = 2;    
                arrowBonus = 5;     
            }
            else{
                crowns = 1;            
            }
        }
        else{
            if(scoreManager.score >= 10 && weapon.arrowCount == arrows - 2){
                crowns = 3; 
                arrowBonus = 10;
            }
            else if(scoreManager.score >= 10 && weapon.arrowCount >= arrows - 6){
                crowns = 2;     
                arrowBonus = 5;       
            }
            else{
                crowns = 1;            
            }
        }
        if(crowns > persistentData.getCrowns(level)){
            persistentData.setCrowns(level, crowns);                
        }
        headshotBonus = weapon.headshots;
        health = player.GetComponent<Player>().currentHealth;
        if(health == player.GetComponent<Player>().maxHealth){
            noHitBonus = 20;
        }
        if(weapon.headshots == 1)
            headshotBonus = 5;
        else if (weapon.headshots == 2)
            headshotBonus = 10;

        coins += 10 + headshotBonus + noHitBonus + arrowBonus;
        persistentData.addCoins(coins);
        coins = persistentData.getCoins();
        levelCompleted.showLevelCompleted(headshotBonus, noHitBonus, arrowBonus, crowns, coins);
        soundManager.PauseMusic();
        soundManager.PlayLevelCompleteSfx();
    }

    // Show the main game screen
    public void ShowGameCanvas()
    {
        SetCanvasVisibility(pauseCanvas, false);
        SetCanvasVisibility(gameoverCanvas, false);
        SetCanvasVisibility(levelCompletedCanvas, false);
        SetCanvasVisibility(levelGame, true);
    }

    // Set the visibility of a canvas
    private void SetCanvasVisibility(GameObject canvas, bool isVisible)
    {
        if (canvas != null)
        {
            canvas.SetActive(isVisible);
        }
    }

    public void BackToHome()
    {
        ShowHomeCanvas();
        soundManager.ResumeMusic();
    }

    public void ExitGame()
    {
        Debug.Log("Game is exiting.");
        Application.Quit();
    }

    public void ResumeGame()
    {
        if(soundManager == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if(obj.name == "SoundManager"){
                    soundManager = obj.GetComponent<SoundManager>();
                }
            }
        }

        Time.timeScale = 1f;
        ShowGameCanvas();
        soundManager.ResumeMusic();
    }

    public void RestartGame()
    {
        if(soundManager == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if(obj.name == "SoundManager"){
                    soundManager = obj.GetComponent<SoundManager>();
                }
            }
        }
        
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level" + level);
        soundManager.ResumeMusic();
    }


    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
        soundManager.ResumeMusic();
    }

    public void Pause(){
        Time.timeScale = 0f;
        ShowPauseCanvas();
        soundManager.PauseMusic();

        if(soundManager == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if(obj.name == "SoundManager"){
                    soundManager = obj.GetComponent<SoundManager>();
                }
            }
        }
    }

    public void NextLevel(){
        if(level < 10){
            Time.timeScale = 1f;
            level++;
            persistentData.currentLevel = level;
            SceneManager.LoadScene("Level" + level);
            soundManager.ResumeMusic();
        }
    }

    public void showSettings(){
        if(settingsCanvas == null){
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp){
                if(obj.name == "SettingsCanvas"){
                    settingsCanvas = obj;
                }
            }
        }

        pauseCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
        
    }

    public void showPrologue()
    {
        if (prologueCanvas == null)
        {
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject obj in temp)
            {
                if (obj.name == "PrologueCanvas")
                {
                    prologueCanvas = obj;
                }
            }
        }
        prologueCanvas.SetActive(true);
    }

    public void closePrologue()
    {
        prologueCanvas.SetActive(false);
        PlayerPrefs.SetString(PrologueShownKey, "true");
        PlayerPrefs.Save();  // Ensures the data is written immediately
        Time.timeScale = 1f;
    }
}
