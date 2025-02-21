using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects/InventorySO")]
public class InventorySO : ScriptableObject
{
    [SerializeField] public List<ItemSO> items;

    [SerializeField] public ItemSO equipedItem;

}


