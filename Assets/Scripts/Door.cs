using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameManagerSO gameManager;

    [SerializeField]
    private int nextSceneIndex;

    [SerializeField]
    private Vector3 nextScenePosition;

    [SerializeField]
    private Vector2 nextSceneRotation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            gameManager.LoadNewScene(nextScenePosition, nextSceneRotation, nextSceneIndex);
        }
    }
}
