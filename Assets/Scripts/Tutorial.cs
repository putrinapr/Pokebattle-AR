using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public string playGameLevel;

    public void PlayGame()
    {
        Application.LoadLevel(playGameLevel);
    }
}
