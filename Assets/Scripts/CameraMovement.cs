using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Vector3 _startPosition;

    void Start () 
    {
        _startPosition = transform.position;
    }
 
    void Update()
    {
        transform.position = _startPosition + new Vector3(0.0f, 0.0f, Mathf.Sin(Time.time) * 0.3f);
    }
}
