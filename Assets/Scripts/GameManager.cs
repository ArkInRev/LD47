using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public PlayerController playerC;

    public bool playerHasExitedStart = false;
    public int totalSO;


    public ShopItem[] shopUpgrades;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerC = player.GetComponent<PlayerController>();
        totalSO = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public event Action onSOCarriedChange;
    public void SOChange()
    {
        if (onSOCarriedChange != null)
        {
            onSOCarriedChange();
            //Debug.Log("onSOCarriedChange sent from GM");
        }

    }

    public event Action onPlayerExitsStart;
    public void PlayerExitsStart()
    {
        playerHasExitedStart = true;
        if (onPlayerExitsStart != null)
        {
            Debug.Log("GM Notifies Player Exiting Start");
            
            onPlayerExitsStart();
        }

    }

    public event Action onSOTotalChange;
    public void SOTotalChange()
    {
        if (onSOTotalChange != null)
        {
            onSOTotalChange();
            //Debug.Log("onSOCarriedChange sent from GM");
        }

    }



}
