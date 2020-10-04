using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
   // public ShopItem thisUpgrade;
    public int upgradeItemInt;

    public TMP_Text upgradeCost;
    public TMP_Text upgradeName;
    public Button thisButton;
    private bool canPurchase = false;

    private PlayerController pc;
    [SerializeField]
   // private ShopManager sm;


    // Start is called before the first frame update
    void Start()
    {
        upgradeName.text = GameManager.Instance.shopUpgrades[upgradeItemInt].upgradeName; 
        upgradeCost.text = GameManager.Instance.shopUpgrades[upgradeItemInt].upgradeCost.ToString();
        GameManager.Instance.onSOTotalChange += OnSOTotalChange;
        GameManager.Instance.onVignettePurchased += OnVignettePurchased;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanPurchaseUpgrade()
    {
        canPurchase = ((GameManager.Instance.shopUpgrades[upgradeItemInt].upgradeCost <= GameManager.Instance.totalSO) && (!GameManager.Instance.shopUpgrades[upgradeItemInt].purchased));
        

        //return ((GameManager.Instance.shopUpgrades[upgradeItemInt].upgradeCost < GameManager.Instance.totalSO)); //&& (!sm.purchasedUpgrades[upgradeItemInt]));

        return canPurchase;
    }


    private void OnSOTotalChange()
    {
        //SOCarriedText.text = GameManager.Instance.playerC.soCarried.ToString();
        if (CanPurchaseUpgrade())
        {
            thisButton.interactable = true;
        } else
        {
            thisButton.interactable = false;
        }
        
        Debug.Log("onSOTotalChanged in Upgrade Button: " + this.name);
    }

    public void OnVignettePurchased(GameObject purchased, int shopInt)
    {
        if (shopInt == upgradeItemInt)
        {
            thisButton.interactable = false;
        }
    }


    private void OnDisable()
    {
        GameManager.Instance.onSOTotalChange -= OnSOTotalChange;
    }

}
