using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Vuforia;

public class ARInteraction : MonoBehaviour
{
    // Use this for initialization
    string[] target = { "RED10", "RED20", "RED30", "GRN10", "GRN20", "GRN30", "BLU10", "BLU20", "BLU30" };
    int[] attack = { 10, 20, 30, 10, 20, 30, 10, 20, 30 };
    string[] element = { "red", "red", "red", "green", "green", "green", "blue", "blue", "blue" };
    int player1_health = 100;
    int player2_health = 100;
    int[] player = { };
    bool attacked = true; // tadinya false
    bool win = false;
    public SimpleHealthBar P1Health;
    public SimpleHealthBar P2Health;
    public GameManager theGameManager;
    public NextRound nextRoundButton;
    public NextRoundOne roundDraw;
    public StartRound roundStart;
    public P1WinRound P1Win;
    public P2WinRound P2Win;
    public AudioSource attackSound;
    public AudioSource soundtrack;
    public AudioSource victory;

    void Start()
    {
        
    }

    public void SetFalse()
    {
        attacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> tbs = sm.GetActiveTrackableBehaviours();

        if (player.Length == 2 && !attacked && !win)
        {
            attackSound.Play();
            attacks(player[0], player[1]);
        }
        else if(!win)
        {
            float pastdist = float.MaxValue;
            int temp;
            foreach (TrackableBehaviour tb in tbs) //detect marker yg lg muncul
            {
                string name = tb.TrackableName;
                int[] res = { Array.IndexOf(target, name) };
                player = player.Union(res).ToArray(); //isinya array res msk ke player tp ga, kalo udh ada (biar ga duplikat)
                float Dist = Vector3.Distance(Camera.main.transform.position, tb.transform.position);
                if (Dist < pastdist)
                {
                    pastdist = Dist;
                    temp = player[0];
                    player[0] = Array.IndexOf(target, name);
                    if (player.Length == 2)
                    {
                        player[1] = temp;
                    }
                }
                
            }
        }
    }

    public void reset()
    {
        win = false;
        player = new int[] { };
        attacked = true;
        player1_health = 100;
        P1Health.UpdateBar(100, 100);
        player2_health = 100;
        P2Health.UpdateBar(100, 100);

        if (victory.isPlaying)
        {
            victory.Stop();
        }

        if (!soundtrack.isPlaying)
        {
            soundtrack.Play();
        }
        //P1Win.gameObject.SetActive(false);
        //P2Win.gameObject.SetActive(false);
        //roundStart.gameObject.SetActive(true);
    }

    public void next_round()
    {
        /*if (player1_health < player2_health)
        {
           Debug.Log("Player 2 win!");
            theGameManager.PlayerTwoWin();
        }
        else
        {
            Debug.Log("Player 1 win!");
            theGameManager.PlayerOneWin();
        }*/
        player = new int[] { };
        attacked = false;
    }

    void condwin(int player)
    {
        win = true;

        if (soundtrack.isPlaying)
        {
            soundtrack.Stop();
        }

        victory.Play();

        if (player == 1)
        {
            theGameManager.PlayerTwoWin();
        }
        else
        {
            theGameManager.PlayerOneWin();
        }
       
        //show win
    }

    int modifier(string element1, string element2)
    {
        int modifier = 0;
        if (element1 == "red" && element2 == "green")
        {
            modifier = 10;
        }
        else if (element1 == "red" && element2 == "blue")
        {
            modifier = -10;
        }
        else if (element1 == "red" && element2 == "red")
        {
            modifier = 0;
        }
        else if (element1 == "green" && element2 == "blue")
        {
            modifier = 10;
        }
        else if (element1 == "green" && element2 == "red")
        {
            modifier = -10;
        }
        else if (element1 == "green" && element2 == "green")
        {
            modifier = 0;
        }
        else if (element1 == "blue" && element2 == "red")
        {
            modifier = 10;
        }
        else if (element1 == "blue" && element2 == "green")
        {
            modifier = -10;
        }
        else if (element1 == "blue" && element2 == "blue")
        {
            modifier = 0;
        }
        return modifier;
    }

    string elementwin(string element1, string element2)
    {
        string elements ="";
        if (element1 == "red" && element2 == "green")
        {
            elements = element1;
        }
        else if (element1 == "red" && element2 == "blue")
        {
            elements = element2;
        }
        else if (element1 == "red" && element2 == "red")
        {
            elements = null;
        }
        else if (element1 == "green" && element2 == "blue")
        {
            elements = element1;
        }
        else if (element1 == "green" && element2 == "red")
        {
            elements = element2;
        }
        else if (element1 == "green" && element2 == "green")
        {
            elements = null;
        }
        else if (element1 == "blue" && element2 == "red")
        {
            elements = element1;
        }
        else if (element1 == "blue" && element2 == "green")
        {
            elements = element2;
        }
        else if (element1 == "blue" && element2 == "blue")
        {
            elements = null;
        }
        return elements;
    }

    void conddraw()
    {
        Debug.Log("Player Draw");
        roundDraw.gameObject.SetActive(true);
        nextRoundButton.gameObject.SetActive(true);
        //next_round();
    }

    void attacks(int player_monster1, int player_monster2)
    {
        player = new int[] { };
        attacked = true;
        int att = (attack[player_monster1] + modifier(element[player_monster1], element[player_monster2])) - attack[player_monster2];

        if (att > 0)
        {
            player2_health -= att;
            P2Health.UpdateBar(player2_health, 100);
            Debug.Log("P2 Health: " + player2_health);
        }
        else if (att < 0)
        {
            player1_health += att;
            P1Health.UpdateBar(player1_health, 100);
            Debug.Log("P1 Health: " + player1_health);
        }
        else
        {
            string elt = elementwin(element[player_monster1], element[player_monster2]);
            if (elt == element[player_monster1])
            {
                player2_health -= 10;
                P2Health.UpdateBar(player2_health, 100);
                Debug.Log("P2 Health: " + player2_health);
                P1Win.gameObject.SetActive(true);
                nextRoundButton.gameObject.SetActive(true);
            }
            else if (elt == element[player_monster2])
            {
                player1_health -= 10;
                P1Health.UpdateBar(player1_health, 100);
                Debug.Log("P1 Health: " + player1_health);
                P2Win.gameObject.SetActive(true);
                nextRoundButton.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("DRAWWWWWWW");
                conddraw();
            }
        }

        if (player1_health <= 0)
        {
            condwin(1);
        }
        else if( player2_health <= 0)
        {
            condwin(2);
        }
        else
        {
            if (att < 0)
            {
                Debug.Log("Player 2 wins this round!");
                P2Win.gameObject.SetActive(true);
                nextRoundButton.gameObject.SetActive(true);
            }
            else if (att > 0)
            {
                Debug.Log("Player 1 wins this round!");
                P1Win.gameObject.SetActive(true);
                nextRoundButton.gameObject.SetActive(true);
            }

            
            //next_round();
        }
       Debug.Log("Attack! " + target[player_monster1] + " and " + target[player_monster2]);
    }
}
