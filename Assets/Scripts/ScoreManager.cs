using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public static int score;

    public GameObject ScoreText;
    TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = ScoreText.GetComponent<TextMeshProUGUI>();

        score = 0;
    }

    void Update()
    {
        scoreText.text = "" + score;
    }
}
