using UnityEngine;

public interface IInteractable
{
    void Interact();

    public Transform transform { get; }
}
