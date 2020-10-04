using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopUpgradeItem", menuName = "Shop/Item", order = 1)]
public class ShopItem : ScriptableObject
{
    public int upgradeCost;
    public string upgradeName;
    public GameObject firstUpgrade;
    public GameObject secondUpgrade;
    public GameObject randomEnemy;
    public bool purchased;
}
