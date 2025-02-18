using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points;
    public Vector3[] Points => points;

    public Vector3 ActualPosition { get; set; }
    private bool gameStarted;

    private void Start()
    {
        gameStarted = true;
        ActualPosition = transform.position;
    }

    public Vector3 GetMovementPosition(int index)
    {
        //Debug.Log("Get Movement Position: Actual position ->" + ActualPosition + " Position que queremos ir: " + points[index]);
        return ActualPosition + points[index]; // Para saber la posicion del punto al cual nos queremos mover
    }

    private void OnDrawGizmos()
    {
        if (gameStarted == false && transform.hasChanged)
        {
            ActualPosition = transform.position;
        }
        
        if (points == null || points.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(points[i] + ActualPosition, 0.5f);
            if (i < points.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + ActualPosition, points[i + 1] + ActualPosition);
            }
        }
    }
}
