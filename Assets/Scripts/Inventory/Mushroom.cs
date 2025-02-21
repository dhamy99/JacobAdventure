using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Item
{
    public override void Interact()
    {
        this.gameObject.SetActive(false);

        base.Interact();
    }
}
