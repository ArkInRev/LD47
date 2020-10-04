using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    private bool zombiesPurchased = false;
    private bool zombiesUpgraded = false;
    private bool zombieBossSpawner = false;

    private bool skeletonsPurchased = false;
    private bool skeletonsUpgraded = false;
    private bool skeletonBossSpawner = false;

    private bool ghostsPurchased = false;
    private bool ghostsUpgraded = false;
    private bool ghostBossSpawner = false;

    public ShopItem[] shopUpgrades;
    public bool[] purchasedUpgrades;



    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 9; i++)
        {
            purchasedUpgrades[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    

}
