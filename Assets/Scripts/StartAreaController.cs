using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAreaController : MonoBehaviour
{
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player exiting start, calling GM");
            GameManager.Instance.PlayerExitsStart();
        }
    }
}
