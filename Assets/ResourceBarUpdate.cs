using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourceBarUpdate : MonoBehaviour
{
    private Slider _slider;
    private PlayerHealth _playerHealth;

	void Start ()
	{
	    _playerHealth = FindObjectOfType<PlayerHealth>();
	    _slider = GetComponentInChildren<Slider>();

	    if (_slider == null)
	        _slider = gameObject.AddComponent<Slider>();
	}
	
	// Update is called once per frame
    void Update()
    {
        _slider.value = MapValue(_playerHealth.CurrentHealth, 0, _playerHealth.StartingHealth, 0, 1);
    }

    private float MapValue(float value,float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return (value - inputMin) * (outputMax - outputMin) / (inputMax - inputMin) + outputMin;
    }
}
