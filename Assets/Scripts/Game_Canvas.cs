using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class Game_Canvas : MonoBehaviour
{
    [SerializeField] GameObject Pause_Button;
    [SerializeField] YandexSDK YandexAD;
    [SerializeField] GameObject Win_Panel;
    [SerializeField] GameObject Pause_Panel;
    [SerializeField] GameObject Lose_Panel;

    [SerializeField] Text Number_Of_BulletsText;

    private bool pausePanelIsOpen = false;

    [HideInInspector] public static UnityEvent PauseGame = new UnityEvent();
    [HideInInspector] public static UnityEvent PlayGame = new UnityEvent();

    private void Awake()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        Player.BulletCounter.AddListener(PlayerBulletCounter);
        Player.PlayerIsDead.AddListener(DeadDelay);
        Gates.PlayerIsWin.AddListener(OpenWinPanel);
    }

    private void Start()
    {
        YandexAD.ShowInterstitial();
    }


    private void PlayerBulletCounter(int curentBulletsNumber)
    {
        Number_Of_BulletsText.text = curentBulletsNumber.ToString();
    }


    public void DeadDelay()
    {
        Invoke("OpenYouLosePanel", 1.5f);
    }

    public void WinDelay()
    {
        Invoke("OpenWinPanel", 1.5f);
    }

    public void OpenAndClosePausePanel()
    {
        if (!pausePanelIsOpen)
        {
            Pause_Panel.SetActive(true);
            Pause_Button.SetActive(false);
            pausePanelIsOpen = true;
            PauseGame.Invoke();
            Time.timeScale = 0;
        }
        else if (pausePanelIsOpen)
        {
            Pause_Panel.SetActive(false);
            Pause_Button.SetActive(true);
            pausePanelIsOpen = false;
            PlayGame.Invoke();
            Time.timeScale = 1;
        }
    }

    public void OpenYouLosePanel()
    {
        Lose_Panel.SetActive(true);
        YandexAD.ShowInterstitial();
    }
    public void OpenWinPanel()
    {
        Win_Panel.SetActive(true);
        YandexAD.ShowInterstitial();
    }

    public void RestartLevel()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNumber);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void ContinueGame()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNumber + 1);
        Time.timeScale = 1;
    }
}
