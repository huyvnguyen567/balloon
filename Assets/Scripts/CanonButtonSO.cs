using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CannonButton", menuName = "Custom/CannonButton")]
public class CanonButtonSO : ScriptableObject
{
    public string CannonName;
    public int price;
    public bool isPurchased;
    public Sprite sprite;
    public GameObject prefab;

    public void UpdateIsPurchasedStatus()
    {
        isPurchased = PlayerPrefs.GetInt(CannonName + "_IsPurchased", 0) == 1;
    }
}
