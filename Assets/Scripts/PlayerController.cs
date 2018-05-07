using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 2f;

    private PlayerMotor motor;


    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        //Cálculo de velocidad de movimiento (3D vector)
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        //Vector de movimiento final
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //Aplicamos el movimiento
        motor.Move(_velocity);

        //Calculamos rotación eje Y (3D vector)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Aplicamos rotación
        motor.Rotate(_rotation);

        //Calculamos rotación eje X (3D vector)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity; //AÑADIDO


        //Aplicamos rotación de cámara
        motor.RotateCamera(_cameraRotationX);
    }
}
