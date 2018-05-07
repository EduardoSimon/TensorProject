using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPower : Power, IPower
{
    public void Shoot()
    {
        Debug.Log("Disparando GUNPOWER " + this.range + " " + this.damage);
    }
}
