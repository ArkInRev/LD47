using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructAfter : MonoBehaviour
{
    private float timeAlive;
    public float timeToLive;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeAlive += Time.fixedDeltaTime;
        if (timeAlive > timeToLive)
        {
            Destroy(this);
        }
    }
}
