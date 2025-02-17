using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum MovementDirection
{
    Horizontal,
    Vertical
}

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private MovementDirection movementDirection; // Dirección del movimiento - Quitar
    [SerializeField] protected float speed;

    public Vector3 DestinationPoint => waypoint.GetMovementPosition(currentWaypointIndex);

    protected Waypoint waypoint;
    protected int currentWaypointIndex;
    protected Vector3 lastPosition;
    // protected Animator animator;

    private void Start()
    {
        currentWaypointIndex = 0;
        waypoint = GetComponent<Waypoint>();
    }

    private void Update()
    {
        MoveCharacter();
        RotateCharacter();
        if (CheckDestinationWaypointReached())
        {
            UpdateCurrentWaypointIndex();
        }
    }

    private void MoveCharacter()
    {
        //Debug.Log("Moving from: " + transform.position);
        transform.position = Vector3.MoveTowards(transform.position, DestinationPoint, speed * Time.deltaTime);
        //Debug.Log("Moving to: " + DestinationPoint);
    }

    private bool CheckDestinationWaypointReached()
    {
        float distanceToWaypoint = (transform.position - DestinationPoint).magnitude; // Magnitud de la distancia entre el punto actual y el punto de destino
        if (distanceToWaypoint < 0.1f) // Estamos cerca del punto de destino
        {
            lastPosition = transform.position;
            return true;
        }
        return false;
    }

    private void UpdateCurrentWaypointIndex()
    {
        if (currentWaypointIndex == waypoint.Points.Length - 1) // Llegamos al final del camino -> Loop 
        {
            currentWaypointIndex = 0;
        }
        else
        {
            if (currentWaypointIndex < waypoint.Points.Length - 1) // Para no pasarnos del tamaño del array
            {
                currentWaypointIndex++;
            }
        }
    }

    private void RotateCharacter()
    {
        if (movementDirection != MovementDirection.Horizontal) return;
        if (DestinationPoint.x > lastPosition.x) // Está yendo a la derecha
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // protected virtual void RotateCharacter()
    // {
        
    // }

    protected virtual void RotateCharacterVertical()
    {
        
    }

}
