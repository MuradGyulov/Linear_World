using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu_Canvas : MonoBehaviour
{
    [SerializeField] GameObject Levels_Panel;
    [SerializeField] GameObject Settings_Panel;
    [SerializeField] Music_Player MusicPlayer;

    [SerializeField] Slider Music_Slider;
    [SerializeField] Slider Sounds_Slider;

    [SerializeField] Button[] Levels_Buttons = new Button[19];


    private bool LevelsPanelIsOpen = false;
    private bool SettingsPenelIsOpen = false;
    private int playerSoundsSettings;
    private int completedLevels;

    private int playerSoundsVolumeSettings = 0;
    private int playerMusicVolumeSettings = 0;


    private void Start()
    {
        completedLevels = PlayerPrefs.GetInt("Completed Levels");
        foreach(Button button in Levels_Buttons)
        {
            button.interactable = false;
        }

        for (int i = 0; i != completedLevels; i++)
        {
            Levels_Buttons[i].interactable = true;
        }


        playerSoundsVolumeSettings = PlayerPrefs.GetInt("Player Sounds Setting");
        if(playerSoundsVolumeSettings == 1)
        {
            Sounds_Slider.value = PlayerPrefs.GetFloat("My Sounds Volume");
        }
        else
        {
            Sounds_Slider.value = 0.8f;
        }

        MusicPlayer = GameObject.FindGameObjectWithTag("Music Player").GetComponent<Music_Player>();
        playerMusicVolumeSettings = PlayerPrefs.GetInt("Player Music Settings");
        if(playerMusicVolumeSettings == 1)
        {
            Music_Slider.value = PlayerPrefs.GetFloat("My Music Volume");
            MusicPlayer.changeVolume(PlayerPrefs.GetFloat("My Music Volume")); 
        }
        else
        {
            Music_Slider.value = 0.2f;
            MusicPlayer.changeVolume(0.2f);
        }
    }

    public void SoundsSliderController()
    {
        float soundsVolume = Sounds_Slider.value;
        PlayerPrefs.SetFloat("My Sounds Volume", soundsVolume);
        playerSoundsSettings = 1;
        PlayerPrefs.SetInt("Player Sounds Setting", playerSoundsSettings);
    }

    public void MusicSliderControlller()
    {
        float musicVolume = Music_Slider.value;
        MusicPlayer.changeVolume(musicVolume);
        PlayerPrefs.SetFloat("My Music Volume", musicVolume);
        playerMusicVolumeSettings = 1;
        PlayerPrefs.SetInt("Player Music Settings", playerMusicVolumeSettings);
    }

    public void OpenAndCloseLevelsPanel()
    {
        if (!LevelsPanelIsOpen)
        {
            Levels_Panel.SetActive(true);
            LevelsPanelIsOpen = true;
        }
        else if (LevelsPanelIsOpen)
        {
            Levels_Panel.SetActive(false);
            LevelsPanelIsOpen = false;
        }
    }

    public void OpenAndClooseSettingsPanel()
    {
        if (!SettingsPenelIsOpen)
        {
            Settings_Panel.SetActive(true);
            SettingsPenelIsOpen = true;
        }
        else if (SettingsPenelIsOpen)
        {
            Settings_Panel.SetActive(false);
            SettingsPenelIsOpen = false;
        }
    }

    public void SceneLoader(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
