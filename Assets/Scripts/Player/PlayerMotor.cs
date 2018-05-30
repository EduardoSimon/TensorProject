using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    [SerializeField]
    private float cameraRotationLimit = 90f;

    private Rigidbody rb;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; //Cursor ratón siempre en el centro invisible
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Obtiene un vector de movimiento
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Obtiene un vector de rotación
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Obtiene un vector de rotación para la cámara
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement() {

        if (velocity != Vector3.zero) {

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotation() {

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if (cam != null)
        {
            // Set our rotation and clamp it. //We multiply by -1 in order to get the right turning direction and not inverted.
            currentCameraRotationX += cameraRotationX * -1;

            //We clamp it to avoid intersections between our body and our limbs.
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Apply our rotation to the transform of our camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }
}
