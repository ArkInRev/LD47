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

    public Transform playerRespawnLocation;

    public ShopItem[] shopUpgrades;

    public GameObject entityHolder;

    public List<GameObject> randomEnemies;

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
        ResetShopUpgrades();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyAllInstantiatedEntities(GameObject parent)
    {
        if (parent.transform.childCount > 0)
        {
            foreach (Transform child in parent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

        }
    }

    public void ShopItemPurchased(int itemNum)
    {
        shopUpgrades[itemNum].purchased = true;
        totalSO -= shopUpgrades[itemNum].upgradeCost;
        SOTotalChange();

        vignettePurchased(shopUpgrades[itemNum].firstUpgrade, itemNum);
        vignettePurchased(shopUpgrades[itemNum].secondUpgrade, itemNum);

        randomEnemies.Add(shopUpgrades[itemNum].randomEnemy);
    }

    public void ResetShopUpgrades()
    {
        foreach(ShopItem items in shopUpgrades)
        {
            items.purchased = false;
        }
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
            //Debug.Log("GM Notifies Player Exiting Start");
            
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

    public event Action onShopEnable;
    public void EnableShop()
    {
        if (onShopEnable != null)
        {
            onShopEnable();
            //Debug.Log("onSOCarriedChange sent from GM");
        }

    }

    public event Action onGameStart;
    public void GameStart()
    {
        /// things to do
        /// tell vignettes to clear
        /// tell vignettes to respawn
        /// GameStart below to 
        DestroyAllInstantiatedEntities(entityHolder);
        Cursor.lockState = CursorLockMode.Locked;


        if (onGameStart != null)
        {
            onGameStart();
            //Debug.Log("onSOCarriedChange sent from GM");
        }

    }

    public event Action onResetPlayer;
    public void ResetPlayer()
    {
        if (onResetPlayer != null)
        {
            onResetPlayer();
           // Debug.Log("onResetPlayer sent from GM");
        }

    }

    public event Action onNextLevel;
    public void doNextLevel()
    {
        if (onNextLevel != null)
        {
            onNextLevel();
            // Debug.Log("onResetPlayer sent from GM");
        }

    }

    public event Action<GameObject,int> onVignettePurchased;
    public void vignettePurchased(GameObject vigPurchased, int shopItem)
    {
        if (onVignettePurchased != null)
        {
            onVignettePurchased(vigPurchased, shopItem);
            // Debug.Log("onResetPlayer sent from GM");
        }

    }

}
