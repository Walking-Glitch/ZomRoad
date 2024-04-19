using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBridge : MonoBehaviour
{
    private Turret turret;
    void Start()
    {
        turret = GetComponentInChildren<Turret>();
    }

    // Update is called once per frame
    public void FireTurretBrige()
    {
        turret.FireTurret();
    }
}
