using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject mobileControlPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject winPanel;
    [Space(12)]
    [SerializeField] private GameObject pauseButton;
    [Space(20)]

    private bool IsTablet = false;
    private bool IsMobile = false;

    private void Start()
    {
        IsTablet = YandexGame.EnvironmentData.isTablet;
        IsMobile = YandexGame.EnvironmentData.isMobile;

        if(IsMobile || IsTablet) { mobileControlPanel.SetActive(true); }

        Player.PlayerIsDead.AddListener(DeadDelay);
        Player.PlayerIsWin.AddListener(WinDelay);
    }

    private void DeadDelay() => Invoke("Dead", 2f);
    private void WinDelay() => Invoke("Win", 2f);

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    private void Dead()
    {
        Time.timeScale = 0;
        deadPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    private void Win()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        pauseButton.SetActive(true);
    }

    public void Restart()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }
}
