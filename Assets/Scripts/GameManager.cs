using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public NextRoundOne Player1;
    public NextRoundOne Player2;
    public NextRound MainMenuButton;
    public NextRound NextRoundButton;
    public P1WinRound P1WR;
    public P2WinRound P2WR;

    public void PlayerOneWin()
    {
        Player1.gameObject.SetActive(true);
        MainMenuButton.gameObject.SetActive(true);
        P1WR.gameObject.SetActive(false);
        NextRoundButton.gameObject.SetActive(false);
    }

    public void PlayerTwoWin()
    {
        Player2.gameObject.SetActive(true);
        MainMenuButton.gameObject.SetActive(true);
        P2WR.gameObject.SetActive(false);
        NextRoundButton.gameObject.SetActive(false);
    }
}
