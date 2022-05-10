using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Gates : MonoBehaviour
{
    private int completedLevels;
    private int curentSceneIndex;

    private bool gatesCollision = false;

    [HideInInspector] public static UnityEvent PlayerIsWin = new UnityEvent();

    private void Start()
    {
        completedLevels = PlayerPrefs.GetInt("Completed Levels");
        curentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && gatesCollision == false)
        {
            gatesCollision = true;

            if (curentSceneIndex == 20)
            {
                PlayerPrefs.SetInt("Completed Levels", curentSceneIndex);
                SceneManager.LoadScene(0);
            }
            else if(completedLevels < curentSceneIndex)
            {
                PlayerPrefs.SetInt("Completed Levels", curentSceneIndex);
                PlayerIsWin.Invoke();
            }
            else if (completedLevels >= curentSceneIndex)
            {
                PlayerIsWin.Invoke();
            }
        }
    }
}
