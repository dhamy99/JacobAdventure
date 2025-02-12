using UnityEngine;

public interface IInteractable
{
    GameObject GameObject { get; }
    void Interact();

    public Transform transform { get; }
}
