using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulUrnController : MonoBehaviour
{

    private bool playerHasExitedStart = false;
    public SphereCollider soTurnInTrigger;
    private PlayerController pc;

    private float secondsToExit = 8f;
    [SerializeField]
    private float timeInTrigger = 0f;

    private bool exitTheLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        playerHasExitedStart = GameManager.Instance.playerHasExitedStart;
        GameManager.Instance.onPlayerExitsStart += onPlayerExitsStart;
        GameObject player = GameObject.FindWithTag("Player");
        pc = player.GetComponent<PlayerController>();

        timeInTrigger = 0f;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            //The player has entered triger
            if (playerHasExitedStart && pc.soCarried>0)
            {   // player has entered the urn trigger after leaving the start area, and is carrying a soul orb
                exitTheLevel = true;
                GameManager.Instance.totalSO += pc.soCarried;
                pc.soCarried = 0;
                GameManager.Instance.SOChange();
                GameManager.Instance.SOTotalChange();
                GameManager.Instance.ResetPlayer();
                GameManager.Instance.EnableShop();
                Debug.Log("Trying to turn in orbs and get to shop");

            } else // player has not exited or does not have any orbs
            {
                // start a countdown timer
                timeInTrigger = 0f;
                //if the timer expires, do the level exit
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            //the player remains inside
            // is the game paused
            if (true) // update with pause logic
            {
                timeInTrigger += Time.fixedDeltaTime;
            }
            if (timeInTrigger >= secondsToExit)
            {
                exitTheLevel = true;
                Debug.Log("exit the level is now set to true.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            //the player has exited the collider
            // reset the countdown timer
            timeInTrigger = 0f;
        }
    }




    private void onPlayerExitsStart()
    {
        playerHasExitedStart = GameManager.Instance.playerHasExitedStart;
    }

    private void OnDisable()
    {
        GameManager.Instance.onPlayerExitsStart -= onPlayerExitsStart;
    }
}
