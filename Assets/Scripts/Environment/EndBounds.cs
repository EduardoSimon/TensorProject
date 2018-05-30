using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBounds : MonoBehaviour {

    GameObject player;
    PlayerHealth playerHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(ETags.Player.ToString());
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ETags.Player.ToString())
        {
            playerHealth.InstaKill();
        }

    }
}
