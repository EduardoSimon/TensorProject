using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    public Text text;

    private PlayerHealth _playerHealth;

	void Start ()
	{
	    _playerHealth = FindObjectOfType<PlayerHealth>();
	}
	
	void Update ()
	{
	    text.text = _playerHealth.CurrentHealthPercentage.ToString();
	}
}
