using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    public Color startingColor;
    public Color targetColor;
    public float startCameraSize;
    public float targetCameraSize;
    public float focusIncrement;
    Transform player;
    Camera minimapCamera;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag(ETags.Player.ToString()).transform;
        minimapCamera = GetComponent<Camera>();
        StartCoroutine(FocusCamera(focusIncrement));

        if (player == null)
            Debug.LogError("No hay jugador en la escena. Coloca uno.");
	}
	
	public void LateUpdate ()
    {
        //En el eje global la camara esta rotada hacia abajo, por ese usamos los angulos locales. Ya que el local y siempre mira para arriba. 
        //Asignamos la rotacion del player en la y a la camara en la y local para que miren al mismo sitio siempre.
        transform.localEulerAngles = new Vector3(transform.eulerAngles.x, player.eulerAngles.y, transform.eulerAngles.z);
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
	}

    IEnumerator FocusCamera(float percentDelta)
    {
        float percent = 0;
        minimapCamera.orthographicSize = startCameraSize;
        minimapCamera.backgroundColor = startingColor;

        while (percent < 1.0f)
        {
            minimapCamera.backgroundColor = Color.Lerp(startingColor, targetColor, percent);
            minimapCamera.orthographicSize = Mathf.Lerp(startCameraSize, targetCameraSize, percent);
            percent += percentDelta;
            yield return null;
        }

    }
}
