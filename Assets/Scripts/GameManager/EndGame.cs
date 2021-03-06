﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    GameObject player;
    PlayerHealth playerHealth;

    private int highestScore;

    private void Awake()
    {
        LoadPlayerProgress();   
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(ETags.Player.ToString());
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if(playerHealth.CurrentHealth<= 0)
        {
            if (ScoreManager.score > highestScore)
            {
                PlayerPrefs.SetInt("highestScore", ScoreManager.score);
            }

            PlayerPrefs.SetInt("currentScore", ScoreManager.score);

            SceneManager.LoadScene("MenuJAMScore");
        }
    }

    private void LoadPlayerProgress()
    {
        if (PlayerPrefs.HasKey("highestScore"))
        {
            highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    public int GetHighestPlayerScore()
    {
        return highestScore;
    }

    private void OnTriggerEnter(Collider other)
    {   
        if(player)
            playerHealth.InstaKill();
    }
}
