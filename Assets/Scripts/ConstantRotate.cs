using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotate : MonoBehaviour
{
    public float spinSpeed;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}
