using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 6f;
    //[SerializeField]
    public string MainMenu;
    private float timeElapsed;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > delayBeforeLoading)

        {
            Application.LoadLevel(MainMenu);
        }

    }
}