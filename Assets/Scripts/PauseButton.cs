using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public UIManager uiManager;

    public void OnPauseButtonPressed()
    {
        uiManager.ShowPauseCanvas();
        Time.timeScale = 0; // Pause the game
    }
}
