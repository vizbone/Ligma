using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTurret : MonoBehaviour {


    //public float range; //Diameter of where the Turret can detect enemies

    //public TurretValues turretValues;
    //private new CapsuleCollider collider;
    int count;

    private void Start()
    {
        //collider = GetComponent<CapsuleCollider>();
        //collider.isTrigger = true;
        //collider.radius = range / 2;
        count = 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tower" && !other.isTrigger)
        {
            TurretTemplate turret = other.GetComponent<TurretTemplate>();
            turret.turretValues.fireRate = turret.turretValues.fireRate * 2;
            
            /*count--;
            if (count == 0)
            {
                turret.turretValues.fireRate = turret.turretValues.fireRate * 2; count = 3;
            }*/

            Debug.Log(turret.turretValues.fireRate);
            print(other.bounds);
        }
    }
   

}
