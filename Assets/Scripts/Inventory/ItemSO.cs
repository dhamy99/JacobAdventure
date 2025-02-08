using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public float damage;
    public float value;
    public string description;
    public Sprite itemIcon;
}
