using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPower : BasePower, IPower
{
    public void Shoot()
    {
        Debug.Log("Shooting Beam Power " + this.range + " " + this.damage);
    }
}
