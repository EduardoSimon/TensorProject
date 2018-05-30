using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public float radius;
    public float yOffset;
    public Transform centerPoint;
    public float rotSpeed;

    float timer = 0;


	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * rotSpeed;
        Rotate();
        transform.LookAt(centerPoint);
    }

    void Rotate() {

        float x = -Mathf.Cos(timer) * radius;
        float z = Mathf.Sin(timer) * radius;
        Vector3 pos = new Vector3(x, yOffset, z);
        transform.position = pos + centerPoint.position;
    }
}
