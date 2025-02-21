using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySO : ScriptableObject
{
    [NonSerialized] private ItemSO item1;
    [NonSerialized] private ItemSO item2;
    [NonSerialized] private ItemSO item3;
    [NonSerialized] private ItemSO item4;

    [NonSerialized] private ItemSO equipedItem;

}
