﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    Text _highScoreTextView;

    // Start is called before the first frame update
    void Start()
    {
        //load high score display
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
