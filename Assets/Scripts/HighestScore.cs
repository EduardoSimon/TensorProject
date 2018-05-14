using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighestScore : MonoBehaviour {

    public GameObject HighText;
    Text highText;

    void Awake()
    {
        highText = HighText.GetComponent<Text>();
    }

    private void Start()
    {
        SetHighestScoreText();
    }

    public void ResetHighestScore()
    {
        PlayerPrefs.SetInt("highestScore", 0);
        SetHighestScoreText();
    }

    private void SetHighestScoreText()
    {
        highText.text = "" + PlayerPrefs.GetInt("highestScore");
    }
}
