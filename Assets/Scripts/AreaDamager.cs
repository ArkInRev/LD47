using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamager : MonoBehaviour
{
    public float damagePerSecond;
    public SphereCollider sc;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player") // if the thing in the area is the player
        {
            other.GetComponent<PlayerController>().Damage(damagePerSecond * Time.fixedDeltaTime);
        }
    }
}
