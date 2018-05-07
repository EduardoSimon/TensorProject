using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPower : BasePower, IPower
{
    public void Shoot()
    {
        Debug.Log("Disparando GUNPOWER " + this.range + " " + this.damage);
    }
}
