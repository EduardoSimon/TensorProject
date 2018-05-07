using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPower : Power, IPower
{
    public void Shoot()
    {
        Debug.Log("Shooting Beam Power " + this.range + " " + this.damage);
    }
}
