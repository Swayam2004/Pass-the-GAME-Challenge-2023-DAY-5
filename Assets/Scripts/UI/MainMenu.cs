using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject[] Credits;
    public TextMeshProUGUI LevelIndicator;

    public int MaxLevel;

    public void ToggleCredits()
    {
        Credits[0].SetActive(!Credits[0].activeSelf);
        Credits[1].SetActive(!Credits[1].activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        LevelIndicator.text = m.ToString();
    }
    public void Up()
    {
        m++;
        if (m > MaxLevel)
        {
            m = 1;
        }
    }
    public void Down()
    {
        m--;
        if (m<1)
        {
            m = MaxLevel;
        }
    }
    int m=1;
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene("Level " + m);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
