using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBarUpdate : MonoBehaviour
{
    public int ActivePowerIndex;

    private PlayerShooting _playerShooting;
    private Power _power;
    private Slider _slider;

	// Use this for initialization
	void Start ()
	{
	    _playerShooting = FindObjectOfType<PlayerShooting>();
	    _slider = GetComponentInChildren<Slider>();
	    _power = _playerShooting.Powers[ActivePowerIndex];
	}
	
	// Update is called once per frame
	void Update ()
	{
	  _slider.value = MapValue(_power.PowerQuantity, 0, _power.StartPowerQuantity, 0, 1);
	}


    private float MapValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return (value - inputMin) * (outputMax - outputMin) / (inputMax - inputMin) + outputMin;
    }
}
