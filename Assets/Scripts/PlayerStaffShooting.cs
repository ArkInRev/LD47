using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaffShooting : MonoBehaviour
{

    public GameObject projectile;
    public Transform staffTip;

    private bool tryFire1;

    private void Awake()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            tryFire1 = true;
        }

    }

    private void FixedUpdate()
    {
        if (tryFire1)
        {
            GameObject obj = Instantiate(projectile, staffTip.transform.position, staffTip.rotation);
            obj.transform.position = staffTip.position;
            tryFire1 = false;
        }
    }
}