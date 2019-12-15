using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRound : MonoBehaviour
{
    public ARInteraction controller;
    public P1WinRound P1;
    public P2WinRound P2;
    public string playGameLevel;

    public void next_round()
    {
        controller.next_round();
        gameObject.SetActive(false);
        P1.gameObject.SetActive(false);
        P2.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        Application.LoadLevel(playGameLevel);
    }
}
