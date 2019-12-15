using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string playGameLevel;
    public string tutorial;

    public void Tutorial()
    {
        Application.LoadLevel(tutorial);
    }

    public void PlayGame()
    {
        Application.LoadLevel(playGameLevel);
    }
}
