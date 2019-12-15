using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoundOne : MonoBehaviour
{
    public ARInteraction controller;

    public void next_round()
    {
        controller.next_round();
        gameObject.SetActive(false);
    }
}
