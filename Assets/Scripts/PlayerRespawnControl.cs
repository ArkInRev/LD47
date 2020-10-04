using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.playerRespawnLocation = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
