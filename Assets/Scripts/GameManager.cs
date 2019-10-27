//
// File: GameManager.cs
// Project: HalloUEM2019
// Description : GameJam Project
// Author: Jesús Fermín Villar Ramírez (pokoidev)


// MIT License
// © Copyright (C) 2019  pokoidev

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Singleton init
    #region
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    private int level = 0;
    private int score = 0;

    private int scorePerLevel = 20;
    public Text scoreText;
    public Text LevelText;


    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    public void UpdateScore(int _s)
    {
        score += _s;
        scoreText.text = "SCORE: " + score.ToString("0000");
        if (score % scorePerLevel == 0) LevelUp();
    }

    private void Start()
    {
        UpdateScore(0);
    }

    private void LevelUp()
    {
        ++level;
        LevelText.text = "LEVEL: " + level.ToString("00");
        PlayerState.instance.LevelUp();
    }


}
