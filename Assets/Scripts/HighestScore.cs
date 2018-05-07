using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighestScore : MonoBehaviour {

    public GameObject HighText;
    TextMeshProUGUI highText;

    void Awake()
    {
        highText = HighText.GetComponent<TextMeshProUGUI>();
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
