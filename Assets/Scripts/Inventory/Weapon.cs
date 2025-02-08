using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public override void Interact()
    {
        this.gameObject.SetActive(false);
    }
}
