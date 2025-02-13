using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSO scriptableObjectData;
    [SerializeField] private GameManagerSO gameManager;

    public ItemSO ScriptableObjectData { get => scriptableObjectData; set => scriptableObjectData = value; }

    public GameObject GameObject => gameObject;

    public abstract void Interact();
}
